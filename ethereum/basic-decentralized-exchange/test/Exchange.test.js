
const ERC20 = artifacts.require("ERC20");
const Exchange = artifacts.require("Exchange");

// TODO - finish basic tests of all functions
//      - add other tests for other (edge) cases - a test for every case, e.g. rejects withdrawl for insufficient balance, rejects withdrawl from withdrawToken for withdrawing ether, etc.

contract("Exchange", accounts => {

    let token;
    let exchange;

    // Transfer some TOKE to second account (first account already contains total supply)
    //await token.transfer(accounts[1], 1500, { from: accounts[0] });

    beforeEach(async() => {
        // Deploy ERC20 and Exchange contracts
        token = await ERC20.deployed();
        exchange = await Exchange.deployed();
    });

    it("should receive ether correctly (depositEther, getEtherBalance)", async () => {

        const depositAmountWei = web3.utils.toWei("9", "ether");
        await exchange.depositEther({ value: depositAmountWei, from:accounts[0] });
        
        const contractAmountWei = new web3.utils.BN(await exchange.getEtherBalance(accounts[0]));

        assert.equal(depositAmountWei.toString(), contractAmountWei.toString(), "Amount ether sent not correct");

    });

    it("should withdraw ether correctly (withdrawEther)", async () => {
        // Test only checks the ether balance for the user at the smart contract, and not the ether balance at the users actual address
        // This is because with gas, it was hard to get an exact amount to assert. So, only checks the amount withdrawn from the contract.

        // Deposit some ETH to the exchange from account 0 to setup - TODO - needed? above test already deposits
        const depositAmountWei = web3.utils.toWei("9", "ether");
        await exchange.depositEther({ value: depositAmountWei, from:accounts[0] });
        
        const contractAmountUserInitial = new web3.utils.BN(await exchange.getEtherBalance(accounts[0]));

        // Withdraw ETH from exchange back to account, get balances again and check amounts
        const withdrawAmountWei = new web3.utils.BN(web3.utils.toWei("6.5", "ether")); // withdraw 6.5 eth
        await exchange.withdrawEther(withdrawAmountWei, { from:accounts[0] });

        const contractAmountUserFinal = new web3.utils.BN(await exchange.getEtherBalance.call(accounts[0]));

        assert.equal(contractAmountUserFinal.toString(), (contractAmountUserInitial.sub(withdrawAmountWei)).toString(), "Amount wasn't correctly taken from contract");

    });

    it("should receive tokens correctly (depositToken, getTokenBalance)", async () => {
        // Approve token, Deposit x tokens from account, call getTokenBalance, assert result=x
        const depositAmount = 99;
        await token.approve(exchange.address, depositAmount, { from: accounts[0] } );
        await exchange.depositToken(token.address, depositAmount, { from: accounts[0] });

        const tokenBalance = (await exchange.getTokenBalance(token.address, accounts[0]));

        assert.equal(depositAmount, tokenBalance, "Amount of token not correctly deposited");
    });

    it("should withdraw tokens correctly (withdrawToken)", async () => {
        // check initial balance, withdraw an amount form exchange, assert exchange balance has reduced, assert user balance from erc20 balanceOf has increased
        const withdrawAmount = 50;

        const initialExchangeTokenBalance = (await exchange.getTokenBalance(token.address, accounts[0])).toNumber();
        const initialUserTokenBalance = (await token.balanceOf(accounts[0])).toNumber();

        await exchange.withdrawToken(token.address, withdrawAmount);

        const finalExchangeTokenBalance = (await exchange.getTokenBalance(token.address, accounts[0])).toNumber();
        const finalUserTokenBalance = (await token.balanceOf(accounts[0])).toNumber();

        assert.equal(finalExchangeTokenBalance, initialExchangeTokenBalance - withdrawAmount, "Exchange balance didn't reduce correctly");
        assert.equal(finalUserTokenBalance, initialUserTokenBalance + withdrawAmount, "User personal token balance didn't increase correctly");

    });

    // Write tests for remaining - creating orders, filling orders, trade, getOrders (? function doesnt exist yet)

});