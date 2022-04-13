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
    // function cancelOrder(uint id) public 
    // Cancels the order specified by id.

    // Output - CancelOrderOutput.cs

    [Function("cancelOrder", typeof(CancelOrderOutput))]
    public class CancelOrderFunction : FunctionMessage
    {
        [Parameter("uint256", "id", 1)]
        public BigInteger OrderId { get; set; }

    }
}
