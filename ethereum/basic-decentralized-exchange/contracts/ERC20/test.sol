
pragma solidity ^0.8.0;

import "./extensions/IERC20Metadata.sol";

/*
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
*/
contract Test {
    
    //using SafeMath for uint;

    address tokenAddress;
    IERC20Metadata tokenInterface;
    
    uint public amount;

    mapping(uint => TestStruct) structMap;

    struct TestStruct {
        uint a;
        address b;
    }

    constructor(address tokenAddress_) {
        tokenAddress = tokenAddress_;
        tokenInterface = IERC20Metadata(tokenAddress);

        amount = 0;
    }

    function testStructAccess() public returns(string memory) {
        structMap[0] = TestStruct(5, address(0));

        if(structMap[0].a == 5) {
            return "55";
        } else {
            return "1";
        }
    }

    function getAmount() public view returns (uint) {
        return amount;
    }

    /*
    function testOverflow() public returns (uint){
        uint a = 5;
        uint b = 6;

        amount = amount.add(1);

        uint c = a.sub(b); // 5 - 6 = -1, should overflow?
        return c;
    }
    */

    function getTokenName() public view returns (string memory) {
        return tokenInterface.name();
    }

    function getTokenSymbol() public view returns (string memory) {
        return tokenInterface.symbol();
    }


}


