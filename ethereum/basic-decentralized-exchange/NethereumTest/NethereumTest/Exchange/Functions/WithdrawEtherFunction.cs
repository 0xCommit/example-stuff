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
    // function withdrawEther(uint _amount) public {
    // Withdraws the _amount of ether from the SC to the user, if they have it

    // Output - WithdrawEtherOutput.cs

    [Function("withdrawEther", typeof(WithdrawEtherOutput))]
    public class WithdrawEtherFunction : FunctionMessage
    {
        [Parameter("uint256", "_amount", 1)]
        public BigInteger Amount { get; set; }
    }
}
