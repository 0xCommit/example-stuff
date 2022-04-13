using System;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Hex.HexTypes;
using System.Numerics;
using Nethereum.Contracts.Extensions;

using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;

using NethereumTest.ERC20.Functions;
using NethereumTest.ERC20.Outputs;
using NethereumTest.ERC20.Events;

using NethereumTest.Exchange.Functions;
using NethereumTest.Exchange.Outputs;
using NethereumTest.Exchange.Events;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        /*
         *  The main program will be a windows form program, but in this console, we can create all the methods we require
         *      
         *      
         * */

        // GetOrders - DONE
        // GetOrdersCancelled - DONE
        // GetOrdersFilled - DONE
        // GetOrderCount - DONE
        // GetEtherBalance - DONE
        // GetTokenBalance - DONE
        // DepositEther
        // WithdrawEther - DONE.
        // DepositToken - DONE. 
        // WithdrawToken - DONE. 
        // CreateOrder - DONE.
        // CancelOrder - DONE
        // FillOrder - DONE. 

        // Events - Deposit, Withdraw, NewOrder, CancelOrder, Trade - DONE

        // Make generic query method, supply exact type and it does it, instead of repeating same web3 initialisation code - not possible. Will need indepdent methods for each method. Pass in web3 instance, contract address etc.
        // Test to_test - DONE
        // DepositEther, and the changes to SC if necessary (use the fallback function) - changed the SC to use the fallback function, which works and is easier from truffle console for example, but doesn't work from here, weird error when sending the tx.
        //                                  - for now could just give some eth to every account
        //                                      - this makes me think, if the supposed ether balance was e.g. 2 eth, but actually had no ether, what happens when calling the withdrawEther function?
        //                                              - obvs errors out when actually sending ether from the smart contract for invalid ether balance.
        
        
        
        // Move to a proper program - have all methods etc, set the structure up and implement. for depositEther for now, leave blank.

        static void Main(string[] args)
        {
            string tokenContractAddress = "0x928Bb5331795e291bB81d524B24a15A26d66baB1";
            string exchangeContractAddress = "0x1aBd0C6Df12eE1a8982D0eCe617a0e9B2cAE7cC6";

            string accountOne = "0x6b58e09c6926c2dd69F99c6A61b53Cdd26c61c4f";
            string accountTwo = "0xE8ac98E589c6dE82e43993A28baBAE3E818D297b";

            string accountOnePK = "0x26a0f9ef64b37d18f74d6244ee45f939546b90342721a5e527b38515a94795d2";

            //DepositTokenToExchange(exchangeContractAddress, tokenContractAddress, accountOne, accountOnePK, 2300).Wait();
            //WithdrawTokenFromExchange(exchangeContractAddress, tokenContractAddress, accountOne, accountOnePK, 1100).Wait();

            //NewOrderEvent order = CreateOrderExchange(exchangeContractAddress, accountOnePK, tokenContractAddress, 100, tokenContractAddress, 10).Result;
            //Console.WriteLine(order.Id);

            // CancelOrderExchange(exchangeContractAddress, accountOnePK, 1).Wait();

            // FillOrderExchange(exchangeContractAddress, accountOnePK, 2).Wait();

            DepositEthToExchange(exchangeContractAddress, accountOnePK, 2.5m).Wait();
        }

        static async Task DepositEthToExchange(string exchangeAddress, string accountPK, decimal ethAmount)
        {
            Account accountOne = new Account(accountPK, Nethereum.Signer.Chain.Private);
            Web3 accountWeb3 = new Web3(accountOne, "http://localhost:8545");

            accountWeb3.TransactionManager.UseLegacyAsDefault = true; // This for some reason needs to be set to work, lol

            var transaction = await accountWeb3.Eth.GetEtherTransferService().TransferEtherAndWaitForReceiptAsync(exchangeAddress, 2.1m, 500, 500000); // 2.1m = 2.1 eth
        }

        static async Task GetCancelledOrderStatus(string contractAddress, int orderId)
        {
            var web3 = new Web3("http://localhost:8545");

            var functionMsg = new GetCancelledOrdersFunction()
            {
                Id = orderId
            };

            var handler = web3.Eth.GetContractQueryHandler<GetCancelledOrdersFunction>();
            var response = await handler.QueryDeserializingToObjectAsync<CancelledOrderOutput>(functionMsg, contractAddress);

            Console.WriteLine(response.Cancelled);
        }

        static async Task GetOrder(string contractAddress, int orderId)
        {
            var web3 = new Web3("http://localhost:8545");

            var orderFunctionMsg = new GetOrderFunction()
            {
                Id = orderId
            };

            var handler = web3.Eth.GetContractQueryHandler<GetOrderFunction>();
            var response = await handler.QueryDeserializingToObjectAsync<Order>(orderFunctionMsg, contractAddress);

            Console.WriteLine(response.Id);
            
        }

        static async Task WithdrawEther(string exchangeContractAddress, string privateKey, BigInteger etherAmount)
        {
            var account = new Account(privateKey);
            Web3 web3 = new Web3(account, "http://localhost:8545");
            web3.TransactionManager.UseLegacyAsDefault = true;

            var handler = web3.Eth.GetContractTransactionHandler<WithdrawEtherFunction>();
            var withdraw = new WithdrawEtherFunction()
            {
                Amount = Web3.Convert.ToWei(etherAmount)
            };

            var txReceipt = await handler.SendRequestAndWaitForReceiptAsync(exchangeContractAddress, withdraw);
        }

        static async Task DepositTokenToExchange(string exchangeContractAddress, string tokenContractAddress, string accountAddress, string privateKey, BigInteger tokenamount)
        {
            // Check ERC20.allowance for exchange to spend amount of token, if not enough, increase allowance
            // Then call Exchange.depositToken to deposit the tokens to the smart contract

            Web3 web3 = new Web3(new Account(privateKey));
            web3.TransactionManager.UseLegacyAsDefault = true;

            BigInteger currentAllowance = await GetAllowance(tokenContractAddress, accountAddress, exchangeContractAddress);
            if(currentAllowance < tokenamount)
            {
                Console.WriteLine($"Current allowance ({currentAllowance}) is less than required amount ({tokenamount}), increasing allowance");
                BigInteger increaseAmount = tokenamount - currentAllowance;

                await ApproveTokens(tokenContractAddress, privateKey, exchangeContractAddress, (int)increaseAmount);
            }
            else
            {
                Console.WriteLine($"Current allowance ({currentAllowance}) equal to or more than required amount, allowance fine.");
            }

            Console.WriteLine("Depositing tokens.");

            var handler = web3.Eth.GetContractTransactionHandler<DepositTokenFunction>();
            var deposit = new DepositTokenFunction()
            {
                TokenAddress = tokenContractAddress,
                Amount = tokenamount
            };

            var txReceipt = await handler.SendRequestAndWaitForReceiptAsync(exchangeContractAddress, deposit);
            var depositEvent = txReceipt.DecodeAllEvents<DepositEvent>()[0];
            Console.WriteLine("Deposit complete. Event balance: " + depositEvent.Event.Balance);
        }

        static async Task WithdrawTokenFromExchange(string exchangeContractAddress, string tokenContractAddress, string accountAddress, string privateKey, BigInteger tokenAmount)
        {
            // Call withdraw function on smart contract to withdraw token back to user
            Web3 web3 = new Web3(new Account(privateKey));
            web3.TransactionManager.UseLegacyAsDefault = true;

            var handler = web3.Eth.GetContractTransactionHandler<WithdrawTokenFunction>();
            var withdraw = new WithdrawTokenFunction()
            {
                TokenAddress = tokenContractAddress,
                Amount = tokenAmount
            };

            var txReceipt = await handler.SendRequestAndWaitForReceiptAsync(exchangeContractAddress, withdraw);
            var withdrawEvent = txReceipt.DecodeAllEvents<WithdrawEvent>()[0];
            Console.WriteLine("Withdraw complete. Event balance: " + withdrawEvent.Event.Balance);

        }

        static async Task CancelOrderExchange(string exchangeContractAddress, string privateKey, int orderId)
        {
            Web3 web3 = new Web3(new Account(privateKey));
            web3.TransactionManager.UseLegacyAsDefault = true;

            var handler = web3.Eth.GetContractTransactionHandler<CancelOrderFunction>();
            var cancelOrder = new CancelOrderFunction()
            {
                OrderId = orderId
            };

            var txReceipt = await handler.SendRequestAndWaitForReceiptAsync(exchangeContractAddress, cancelOrder);
            var cancelEvent = txReceipt.DecodeAllEvents<CancelOrderEvent>()[0].Event;

            Console.WriteLine(cancelEvent.Id);
        }

        static async Task FillOrderExchange(string exchangeContractAddress, string privateKey, int orderId)
        {
            Web3 web3 = new Web3(new Account(privateKey));
            web3.TransactionManager.UseLegacyAsDefault = true;

            var handler = web3.Eth.GetContractTransactionHandler<FillOrderFunction>();
            var fillOrder = new FillOrderFunction()
            {
                OrderId = orderId
            };

            var txReceipt = await handler.SendRequestAndWaitForReceiptAsync(exchangeContractAddress, fillOrder);
            var tradeEvent = txReceipt.DecodeAllEvents<TradeEvent>()[0].Event;

            Console.WriteLine(tradeEvent.Id);
        }
    

        static async Task<NewOrderEvent> CreateOrderExchange(string exchangeContractAddress, string privateKey, string tokenReceiveAddress, BigInteger amountReceive, string tokenGiveAddress, BigInteger amountGive)
        {
            Web3 web3 = new Web3(new Account(privateKey));
            web3.TransactionManager.UseLegacyAsDefault = true;

            var handler = web3.Eth.GetContractTransactionHandler<CreateOrderFunction>();
            var newOrder = new CreateOrderFunction()
            {
                TokenReceiveAddress = tokenReceiveAddress,
                AmountReceive = amountReceive,
                TokenGiveAddress = tokenGiveAddress,
                AmountGive = amountGive
            };

            var txReceipt = await handler.SendRequestAndWaitForReceiptAsync(exchangeContractAddress, newOrder);
            var orderEvent = txReceipt.DecodeAllEvents<NewOrderEvent>()[0].Event;

            return orderEvent;

        }

        #region ERC-20-Methods
        static async Task ApproveTokens(string contractAddress, string privateKey, string spender, int amount)
        {
            var account = new Account(privateKey);
            Web3 web3 = new Web3(account, "http://localhost:8545");
            web3.TransactionManager.UseLegacyAsDefault = true;

            var handler = web3.Eth.GetContractTransactionHandler<ApproveFunction>();
            var approve = new ApproveFunction()
            {
                Spender = spender,
                Amount = amount
            };

            var txReceipt = await handler.SendRequestAndWaitForReceiptAsync(contractAddress, approve);

            // Get the event that is emitted and display
            var approvalEvent = txReceipt.DecodeAllEvents<ApprovalEvent>();
            Console.WriteLine("Size: " + approvalEvent.Count);
            Console.WriteLine("Event owner: " + approvalEvent[0].Event.Owner);
            Console.WriteLine("Event spender: " + approvalEvent[0].Event.Spender);
            Console.WriteLine("Event value: " + approvalEvent[0].Event.Value);

            Console.WriteLine("Event toString: " + approvalEvent[0].ToString());
            Console.WriteLine("Log toString: " + approvalEvent[0].Log.ToString());
        }

        static async Task TransferFrom(string contractAddress, string privateKey, string sender, string recipient, int amount)
        {
            var account = new Account(privateKey);
            Web3 web3 = new Web3(account, "http://localhost:8545");
            web3.TransactionManager.UseLegacyAsDefault = true;

            var handler = web3.Eth.GetContractTransactionHandler<TransferFromFunction>();
            var transfer = new TransferFromFunction()
            {
                Sender = sender,
                Recipient = recipient,
                Amount = amount
            };

            var txReceipt = await handler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer);
        }

        static async Task<BigInteger> GetAllowance(string contractAddress, string ownerAddress, string spenderAddress)
        {
            var web3 = new Web3("http://localhost:8545");

            var allowanceFunctionMsg = new AllowanceFunction()
            {
                Owner = ownerAddress,
                Spender = spenderAddress
            };

            var handler = web3.Eth.GetContractQueryHandler<AllowanceFunction>();
            var amount = await handler.QueryDeserializingToObjectAsync<AllowanceOutput>(allowanceFunctionMsg, contractAddress);

            return amount.Allowance;
        }

        static async Task GetName(string contractAddress)
        {
            var web3 = new Web3("http://localhost:8545");

            var nameFunction = new NameFunction() { };

            var handler = web3.Eth.GetContractQueryHandler<NameFunction>();
            var response = await handler.QueryDeserializingToObjectAsync<NameOutput>(nameFunction, contractAddress);

            Console.WriteLine(response.Name);
        }

        // Play with interacting with a smart contract. Calls the balanceOf function on the _already deployed_ ERC20 contract (specified by contractAddress) for user userAddress
        static async Task GetUserTokenBalance(string contractAddress, string userAddress)
        {
            var web3 = new Web3("http://localhost:8545");

            var balanceOfFunctionMessage = new BalanceOfFunction()
            {
                Account = userAddress
            };

            var balanceHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
            //var balance = await balanceHandler.QueryAsync<BigInteger>(contractAddress, balanceOfFunctionMessage);

            var balance = await balanceHandler.QueryDeserializingToObjectAsync<BalanceOfOutput>(balanceOfFunctionMessage, contractAddress);

            Console.WriteLine(balance.Balance);
        }

        static async Task TransferTokenBalance(string contractAddress, string privateKey, string toAddress, int amount)
        {
            var account = new Account(privateKey);
            Web3 web3 = new Web3(account, "http://localhost:8545");
            web3.TransactionManager.UseLegacyAsDefault = true;

            var transferHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();
            var transfer = new TransferFunction()
            {
                Recipient = toAddress,
                TokenAmount = amount
            };

            var txReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer);
            Console.WriteLine(txReceipt.GasUsed);
        }

        // Demonstrate creating accounts using private keys, and transferring ether from an account
        static async Task TransferSomeBalances()
        {
            string accountOneAddress = "0x620B8C56B06795aF299Be51B89DF102444c74bD6";
            string accountOnePK = "0x73851017e98c8d3b46d2837c6d33f08f93aaeef3f504dad7b3337ed92d64c7ec";

            string accountTwoAddress = "0xa226f74Eeff8c7e6724aBe7d929dCfa2916bd481";
            string accountTwoPK = "0x3ec81519a8deb066b05128c2d984379031acffbd1d0299319433b6e0467bd9f0";

            Account accountOne = new Account(accountOnePK, Nethereum.Signer.Chain.Private);
            Web3 accountWeb3 = new Web3(accountOne, "http://localhost:8545");
            accountWeb3.TransactionManager.UseLegacyAsDefault = true; // This for some reason needs to be set to work, lol

            var transaction = await accountWeb3.Eth.GetEtherTransferService().TransferEtherAndWaitForReceiptAsync(accountTwoAddress, 2.1m); // 2.1m = 2.1 eth

            // Print account balances
            await GetAccountBalance(accountOneAddress);
            await GetAccountBalance(accountTwoAddress);
        }
        
        // Gets an accounts ether balance
        static async Task GetAccountBalance(string etherAddress)
        {
            var web3 = new Web3("http://localhost:8545");
            var balance = await web3.Eth.GetBalance.SendRequestAsync(etherAddress);

            Console.WriteLine($"Balance in ether: {Web3.Convert.FromWei(balance.Value)}");
        }

        #endregion

    }
}