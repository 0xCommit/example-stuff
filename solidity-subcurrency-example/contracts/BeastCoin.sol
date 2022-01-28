pragma solidity ^0.8.4;

// Contract - BeastCoin
// Members:   public minter (address)
//            public balances (map address-uint)
// Events:    Sent (from, to, amount)
// Errors:    InsufficientBalance (requested, available)
// Functions: public mint (receiver, amount) - mints tokens to the received address. Caller must be the contract deployer.
//            public send (receiver, amount) - sends tokens from caller address to receiver address of specified amount.

contract BeastCoin {

  address public minter; // public address, so accessible from other contracts (even without get methods, as automatically created), however not modifiable once set (as no methods to do so)
  mapping (address => uint) public balances; // mapping of addresses to balances. Acts as a ledger for the coins supply

  // Sent event - notify clients that a transaction was made
  event Sent(address from, address to, uint amount);

  // set the minter address when the contract is deployed, set it to the address that deployed
  constructor() {
    minter = msg.sender;
  }

  // Sends amount of freshly created coins to an address
  // Can only be called by the contract creator, however can be called always
  function mint(address receiver, uint amount) public {
    require(msg.sender == minter);
    balances[receiver] += amount;
  }

  // Return an error object if an error occurs
  error InsufficientBalance(uint requested, uint available);

  // Send an amount of coins from the caller to an address
  function send(address receiver, uint amount) public {
    
    // Check amount sending is within the balance of the sender address
    if (amount > balances[msg.sender]) {
        revert InsufficientBalance({
          requested: amount,
          available: balances[msg.sender]
        });
    }

    // Transfer the balance and emit Sent event
    balances[msg.sender] -= amount;
    balances[receiver] += amount;
    emit Sent(msg.sender, receiver, amount);

  }

}