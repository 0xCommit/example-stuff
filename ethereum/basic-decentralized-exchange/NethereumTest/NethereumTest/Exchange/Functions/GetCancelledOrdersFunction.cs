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
    // mapping(uint => bool)  public ordersCancelled; // Order id to boolean true if cancelled
    // Gets the status (true or false) if an order with the specified ID is cancelled or not.

    // Output - CancelledOrderOutput.cs

    [Function("ordersCancelled", typeof(CancelledOrderOutput))]
    public class GetCancelledOrdersFunction : FunctionMessage
    {
        [Parameter("uint256", "id", 1)]
        public BigInteger Id { get; set; }
    }
}
