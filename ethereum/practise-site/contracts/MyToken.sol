
// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "@openzeppelin/contracts/token/ERC20/IERC20.sol";
import "@openzeppelin/contracts/token/ERC20/extensions/IERC20Metadata.sol";
import "@openzeppelin/contracts/utils/Context.sol";

contract MyToken is Context, IERC20, IERC20Metadata {

  mapping(address => uint256) private _balances;

  mapping(address => mapping(address => uint256)) private _allowances;

  uint256 private _totalSupply;

  string private _name;
  string private _symbol;

  uint public MAX_SUPPLY = 10000; // only 10,000 tokens will ever exist
  uint8 private tokenIncrease = 50; // each free mint gives user 50 tokens

  constructor(string memory name_, string memory symbol_) {
    _name = name_;
    _symbol = symbol_;
    // mint tokens
    _mint(_msgSender(), 9000); // mint 9000 tokens straight away to contract creator
  }

  function mintFreeTokens() public {
      require(_totalSupply + tokenIncrease <= MAX_SUPPLY, "MyToken: all tokens minted already");
      _mint(_msgSender(), tokenIncrease);
  }

  function name() public view virtual override returns (string memory) {
    return _name;
  }

  function symbol() public view virtual override returns (string memory) {
    return _symbol;
  }

  function decimals() public view virtual override returns (uint8) {
    return 18;
  }

  function totalSupply() public view virtual override returns (uint256) {
    return _totalSupply;
  }

  function balanceOf(address account) public view virtual override returns (uint256) {
    return _balances[account];
  }

  function transfer(address recipient, uint256 amount) public virtual override returns (bool) {
    _transfer(_msgSender(), recipient, amount);
    return true;
  }

  function allowance(address owner, address spender) public view virtual override returns (uint256) {
    return _allowances[owner][spender]; // returns the amount of tokens spender is allowed to spend
  }

  function approve(address spender, uint256 amount) public virtual override returns (bool) {
    _approve(_msgSender(), spender, amount);
    return true;
  }

  function _approve(address owner, address spender, uint256 amount) internal virtual {
    require(owner != address(0), "ERC20: approve from the zero address");
    require(spender != address(0), "ERC20: approve to the zero address");

    _allowances[owner][spender] = amount;
     emit Approval(owner, spender, amount);
  }

  function increaseAllowance(address spender, uint256 addedValue) public virtual returns (bool) {
    _approve(_msgSender(), spender, _allowances[_msgSender()][spender] + addedValue);
    return true;
  }

  function decreaseAllowance(address spender, uint256 subtractedValue) public virtual returns (bool) {
    uint256 currentAllowance = _allowances[_msgSender()][spender];
    require(currentAllowance >= subtractedValue, "ERC20: decreased allowance below zero");
    unchecked {
      _approve(_msgSender(), spender, currentAllowance - subtractedValue);
    }

    return true;
  }

  // This function is called from an address that is approved to spend an amount of 'sender's tokens, and can send to anybody
  function transferFrom(address sender, address recipient, uint256 amount) public virtual override returns (bool) {
    uint256 currentAllowance = _allowances[sender][_msgSender()]; // how many tokens the caller is allowed to spend of 'sender's
    if(currentAllowance != type(uint256).max) {
      require(currentAllowance >= amount, "ERC20: transfer amount exceeds allowance");
      unchecked {
         _approve(sender, _msgSender(), currentAllowance - amount); // update the amount spender is allowed to spend of senders
      }
    }

    _transfer(sender, recipient, amount);
    return true;
  }

  function _transfer(address sender, address recipient, uint256 amount) internal virtual {
    require(sender != address(0), "ERC20: transfer from the zero address");
    require(recipient != address(0), "ERC20: transfer to the zero address");

    _beforeTokenTransfer(sender, recipient, amount);

    uint256 senderBalance = _balances[sender];
    require(senderBalance >= amount, "ERC20: transfer amount exceeds balance");
    unchecked {
        _balances[sender] = senderBalance - amount;
    }
    _balances[recipient] += amount;

    emit Transfer(sender, recipient, amount);

     _afterTokenTransfer(sender, recipient, amount);

  }

  function _mint(address account, uint256 amount) internal virtual {
      require(account != address(0), "ERC20: mint to the zero address");

       _beforeTokenTransfer(address(0), account, amount);

      _totalSupply += amount;
      _balances[account] += amount;
       emit Transfer(address(0), account, amount);

       _afterTokenTransfer(address(0), account, amount);
  }

  function _burn(address account, uint256 amount) internal virtual {
      require(account != address(0), "ERC20: burn from the zero address");

       _beforeTokenTransfer(account, address(0), amount);

      uint256 accountBalance = _balances[account];
      require(accountBalance >= amount, "ERC20: burn amount exceeds balance");
      unchecked {
         _balances[account] = accountBalance - amount;
      }
      _totalSupply -= amount;

       emit Transfer(account, address(0), amount);

       _afterTokenTransfer(account, address(0), amount);
  }

  function _beforeTokenTransfer(address from, address to, uint256 amount) internal virtual { }
  function _afterTokenTransfer(address from, address to, uint256 amount) internal virtual { }

}


