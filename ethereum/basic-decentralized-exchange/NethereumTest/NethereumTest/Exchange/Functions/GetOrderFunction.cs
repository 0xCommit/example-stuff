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
    // mapping(uint => Order) public orders; // Map of an order id to the order struct
    // Gets the Order struct with the specified ID

    // Output - Order.cs

    [Function("orders", typeof(Order))]
    public class GetOrderFunction : FunctionMessage
    {
        [Parameter("uint256", "id", 1)]
        public BigInteger Id { get; set; }
    }
}
