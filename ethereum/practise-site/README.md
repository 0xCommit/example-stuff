# practise-site
# My own practise

Used ganache-cli for local Ethereum test network/environment. Used the truffle suite for deploying.

Created an ERC20 token (contract name = MyToken, ticker = COOL), using the official standard OpenZeppelin ERC20 interface, so all appropriate ERC20 methods/functions are implemented.
Then created a basic html and javascript website that contains some functionality to interact with the ERC20 contract, such as checking your own balance, transferring tokens to another address, and minting the tokens.

I did this by myself combining all the stuff learnt in the other tutorials for practice/a more 'complete' project.

The Javascript code uses both the Metamask library that is injected in a website if MM is installed, as well as web3js to interact with the smart contract.

There are a few things that could be added to make the website more complete, e.g. better error handiling etc.