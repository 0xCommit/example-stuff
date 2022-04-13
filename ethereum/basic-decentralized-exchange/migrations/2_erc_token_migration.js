/*
    This migration contract deployes both the ERC20 contract, creating the aTokenName token with symbol=TOKE
                            And it deploys the "Test" contract, passing in the aTokeName contract address to the constructor
*/

const Token = artifacts.require("ERC20");
const TestContract = artifacts.require("test");

module.exports = function (deployer) {
  deployer.deploy(Token, "aTokenName", "TOKE") // Deploy "Token"

  .then(() =>   deployer.deploy(TestContract, Token.address)); // Deploy "TestContract", passing in Token contract address
};
