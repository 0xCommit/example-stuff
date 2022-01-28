const BeastCoin = artifacts.require("BeastCoin");

module.exports = function (deployer) {
  deployer.deploy(BeastCoin);
};
