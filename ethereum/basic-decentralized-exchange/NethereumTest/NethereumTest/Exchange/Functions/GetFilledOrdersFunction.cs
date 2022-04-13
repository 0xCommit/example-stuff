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
    // mapping(uint => bool) public ordersFilled; // Order id to boolean true if order fully filled
    // Gets the status (true or false) if an order with the specified ID has been filled or not

    // Output - FilledOrderOutput.cs

    [Function("ordersFilled", typeof(FilledOrderOutput))]
    public class GetFilledOrdersFunction : FunctionMessage
    {
        [Parameter("uint256", "id", 1)]
        public BigInteger Id { get; set; }
    }
}
