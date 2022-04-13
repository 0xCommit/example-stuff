pragma solidity ^0.8.0;

import "./ERC20/ERC20.sol";

library SafeMath {
    function sub(uint256 a, uint256 b) internal pure returns (uint256) {
      assert(b <= a);
      return a - b;
    }

    function add(uint256 a, uint256 b) internal pure returns (uint256) {
      uint256 c = a + b;
      assert(c >= a);
      return c;
    }
}

// TODO - this only supports fully filling an order, would like to add partial fills. Problem is, division is tricky...
// TODO - test creating an order and filling an order. Right now, anybody could create an order, even if they don't have the tokens (in their wallet or in the SC). What happens
//          when executing a trade? I assume an error there. Test first, then fix...make it so can't create an order if you don't have the tokens in the SC?
//          Lol, already addded a description for this just below, nice.

// With this version of the DEX, and in the github version, anybody can create an order and call the fill orders, even if they have not deposited any funds to the smart contract
//  When the trade function is called, and the balances update (remove/add from seller/buyer), the SafeMath add/sub functions will revert if a number goes -ve, so if
//    somebody did call those functions without any balance, it would revert, and the trade would not happen.
//  However, shouldn't there be a better way? Ensure before the function is called/executed, that both parties have the balance required?
//  Something to test later, and then implement a fix.

contract Exchange is Context {

    using SafeMath for uint;

    mapping(address => mapping(address => uint)) private tokenBalances; // For each token address, a map of a users address to token balance
    address constant ETHER = address(0); // For ether balances, set token address to null address

    // Order mappings. Keep track of all orders, and their status of either cancelled or filled. If either cancelled or filled, order is no longer active
    // All public, so have automatic get methods generated.
    mapping(uint => Order) public orders; // Map of an order id to the order struct
    mapping(uint => bool)  public ordersCancelled; // Order id to boolean true if cancelled
    mapping(uint => bool) public ordersFilled; // Order id to boolean true if order fully filled
    uint public orderCount; // Used for id of orders

    // Events
    event Deposit(address tokenAddress, address userAddress, uint amount, uint balance);
    event Withdraw(address tokenAddress, address userAddress, uint amount, uint balance);
    event NewOrder(uint id, address user, address tokenReceive, uint amountReceive, address tokenGive, uint amountGive, uint timestamp);
    event CancelOrder(uint id, address user, address tokenReceive, uint amountReceive, address tokenGive, uint amountGive, uint timestamp);
    event Trade(uint id, address user, address tokenReceive, uint amountReceive, address tokenGive, uint amountGive, address userFill, uint timestamp); // userFill = taker

    // Order struct
    struct Order {
      uint id; // ID of order
      address creator; // Address of user who created this order
      address tokenAddressReceive; // Token user wants to receive (buy)
      address tokenAddressGive; // Token user wants to give (sell)
      uint amountReceive; // Amount of token to receive (buy)
      uint amountGive; // Amount of token to give (sell)
      uint timestamp; // unix timestamp, from "block.timestamp"

    }

    constructor() {
        orderCount = 0; // First order will have id=1. OrderCount will always be equal to the latest order ID.
     }

    // Fallback function: called if Ether is sent to this contract, and not to a specific method.
    // Used to revert, but made it a depositEther function too. If a user sends ETH straight to this SC, they will increase their sc ETH balance.
    fallback() external payable {
        tokenBalances[ETHER][_msgSender()] = tokenBalances[ETHER][_msgSender()].add(msg.value);
        emit Deposit(ETHER, _msgSender(), msg.value, tokenBalances[ETHER][_msgSender()]);
        //revert();
    }

    function getEtherBalance(address _userAddress) public view returns (uint) {
        return tokenBalances[ETHER][_userAddress];
    }

    function getTokenBalance(address _tokenAddress, address _userAddress) public view returns (uint) {
        require(_tokenAddress != ETHER, "Method is for tokens");
        return tokenBalances[_tokenAddress][_userAddress];
    }

    function depositEther() public {
        //tokenBalances[ETHER][_msgSender()] = tokenBalances[ETHER][_msgSender()].add(msg.value);
        //emit Deposit(ETHER, _msgSender(), msg.value, tokenBalances[ETHER][_msgSender()]);
    }

    function withdrawEther(uint _amount) public {
        require(tokenBalances[ETHER][_msgSender()] >= _amount, "Not enough ether balance");
        tokenBalances[ETHER][_msgSender()] = tokenBalances[ETHER][_msgSender()].sub(_amount);

        payable(_msgSender()).transfer(_amount); // Cast the sender address as a payable address, then call the transfer function, to send the ETH to that address from this SC

        emit Withdraw(ETHER, _msgSender(), _amount, tokenBalances[ETHER][_msgSender()]);
    }

    // User calls this function when depositing a token. The token must be approved to spend the amount, before this function is called.
    // This contract will then actually transfer the tokens from the user to this contract, by calling the transferFrom function. 
    function depositToken(address _tokenAddress, uint _amount) public {
        require(_tokenAddress != ETHER, "Method is for tokens");
        require(ERC20(_tokenAddress).transferFrom(_msgSender(), address(this), _amount), "Not approved for amount"); // If works, token is tx'd to this contract
        
        tokenBalances[_tokenAddress][_msgSender()] = tokenBalances[_tokenAddress][_msgSender()].add(_amount);
        
        emit Deposit(_tokenAddress, _msgSender(), _amount, tokenBalances[_tokenAddress][_msgSender()]);
    }

    function withdrawToken(address _tokenAddress, uint _amount) public {
        require(_tokenAddress != ETHER, "Method is for tokens");
        require(tokenBalances[_tokenAddress][_msgSender()] >= _amount, "Not enough balance");

        tokenBalances[_tokenAddress][_msgSender()] = tokenBalances[_tokenAddress][_msgSender()].sub(_amount);
        assert(ERC20(_tokenAddress).transfer(_msgSender(), _amount)); // If doesn't work, revert.

        emit Withdraw(_tokenAddress, _msgSender(), _amount, tokenBalances[_tokenAddress][_msgSender()]);
    }

    function createOrder(address _tokenReceive, uint _amountReceive, address _tokenGive, uint _amountGive) public {
        orderCount = orderCount.add(1);
        orders[orderCount] = Order(orderCount, _msgSender(), _tokenReceive, _tokenGive, _amountReceive, _amountGive, block.timestamp);

        emit NewOrder(orderCount, _msgSender(), _tokenReceive, _amountReceive, _tokenGive, _amountGive, block.timestamp);
    }

    function cancelOrder(uint _id) public {
        require(_id > 0 && _id <= orderCount, "Order with id does not exist"); // Order with that id actually exists
        Order memory _order = orders[_id];
        require(_order.creator == _msgSender(), "Can only cancel your own order");
        ordersCancelled[_id] = true;
        
        emit CancelOrder(_id, _msgSender(), _order.tokenAddressReceive, _order.amountReceive, _order.tokenAddressGive, _order.amountGive, block.timestamp);
    }

    // This function is called by the trade TAKER (executor), not the creator.
    function fillOrder(uint _id) public {
        require(_id > 0 && _id <= orderCount, "Order with id does not exist");
        require(!ordersCancelled[_id], "Order is cancelled");
        require(!ordersFilled[_id], "Order is already filled");
        
        Order storage _order = orders[_id]; // Again, is this costly? Just above we accessed the struct/struct mapping, and now for the second time. Bit redundant...TODO
        _trade(_order.id, _order.creator, _order.tokenAddressReceive, _order.amountReceive, _order.tokenAddressGive, _order.amountGive);

        ordersFilled[_id] = true;

    }

    // This function will be called by the person taking the other side of the trade, that is, not the person who created the order.
    // tokenReceive refers to the token that the person who created the order is receiving, not the person executing this function. 
    function _trade(uint _orderId, address orderCreator, address _tokenReceive, uint _amountReceive, address _tokenGive, uint _amountGive) internal {
        tokenBalances[_tokenReceive][_msgSender()] = tokenBalances[_tokenReceive][_msgSender()].sub(_amountReceive); // Executor selling tokenReceive, so reduce their balance
        tokenBalances[_tokenReceive][orderCreator] = tokenBalances[_tokenReceive][orderCreator].add(_amountReceive); // Creator of order is receiving/buying tokenReceive, so increase their balance

        tokenBalances[_tokenGive][_msgSender()] = tokenBalances[_tokenGive][_msgSender()].add(_amountGive); // Executor buying tokenGive, so increase their balance
        tokenBalances[_tokenGive][orderCreator] = tokenBalances[_tokenGive][orderCreator].sub(_amountGive); // Creator of order is selling tokenGive, so reduce their balance

        emit Trade(_orderId, orderCreator, _tokenReceive, _amountReceive, _tokenGive, _amountGive, _msgSender(), block.timestamp);
    }


}