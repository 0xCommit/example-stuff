using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum;
using Nethereum.Web3;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

using NethereumTest.Exchange.Outputs;

namespace NethereumTest.Exchange.Functions
{
    // function depositToken(address _tokenAddress, uint _amount) public 
    // User calls this function when depositing a token. The token must be approved to spend the amount, before this function is called.
    // This contract will then actually transfer the tokens from the user to this contract, by calling the transferFrom function. 

    // Output - DepositTokenOutput.cs

    [Function("depositToken", typeof(DepositTokenOutput))]
    public class DepositTokenFunction : FunctionMessage
    {
        [Parameter("address", "_tokenAddress", 1)]
        public string TokenAddress { get; set; }

        [Parameter("uint256", "_amount", 2)]
        public BigInteger Amount { get; set; }
    }
}
