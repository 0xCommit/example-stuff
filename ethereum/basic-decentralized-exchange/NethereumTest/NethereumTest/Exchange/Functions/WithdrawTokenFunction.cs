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
    // function withdrawToken(address _tokenAddress, uint _amount) public {
    // Withdraws the _amount of token with _tokenAddress from the SC to the user, if they have it

    // Output - WithdrawTokenOutput.cs

    [Function("withdrawToken", typeof(WithdrawTokenOutput))]
    public class WithdrawTokenFunction : FunctionMessage
    {
        [Parameter("address", "_tokenAddress", 1)]
        public string TokenAddress { get; set; }

        [Parameter("uint256", "_amount", 2)]
        public BigInteger Amount { get; set; }
    }
}
