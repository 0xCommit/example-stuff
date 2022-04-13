
const ERC20 = artifacts.require("ERC20");

contract("ERC20", accounts => {
    it("should have total supply 100000 TOKE", async () => {
        const instance = await ERC20.deployed();
        const supply = await instance.totalSupply.call();
        assert.equal(supply.valueOf(), 100000);
    });
    
    it("should have token symbol TOKE", async () => {
        const instance = await ERC20.deployed();
        const symbol = (await instance.symbol()).toString();
        assert.equal(symbol, "TOKE");
    });

    it("should transfer coins correctly", async () => {
        // Get intial account balances
        const accountOne = accounts[0];
        const accountTwo = accounts[1];

        const instance = await ERC20.deployed();
        const transferAmount = 1500;

        const accountOneInitialBalance = (await instance.balanceOf(accountOne)).toNumber();
        const accountTwoInitialBalance = (await instance.balanceOf(accountTwo)).toNumber();

        // Transfer from account one to account two
        await instance.transfer(accountTwo, transferAmount, { from: accountOne });

        const accountOneFinalBalance = (await instance.balanceOf(accountOne)).toNumber();
        const accountTwoFinalBalance = (await instance.balanceOf(accountTwo)).toNumber();

        assert.equal(accountOneFinalBalance, accountOneInitialBalance - transferAmount, "Amount not correctly taken from sender");
        assert.equal(accountTwoFinalBalance, accountTwoInitialBalance + transferAmount, "Amount not correctly added to the receiver");

    });

});