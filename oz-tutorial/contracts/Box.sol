// contracts/Box.sol
// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

// Import Auth from access-control subdirectory
//import "./access-control/Auth.sol";
import "@openzeppelin/contracts/access/Ownable.sol";

// Box inherits from the OpenZeppelin Ownable contract
contract Box is Ownable {
    uint256 private _value;
    //Auth private _auth;

    // Emitted when the stored value changes
    event ValueChanged(uint256 value);

    // Stores a new value in the contract
    function store(uint256 value) public onlyOwner {
	_value = value;
        emit ValueChanged(value);
    }

    // Reads the last stored value
    function retrieve() public view returns (uint256) {
        return _value;
    }
}