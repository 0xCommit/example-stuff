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
    // uint public orderCount; // Used for id of orders
    // Gets the orderCount value. orderCount will have the id of the latest created order.

    // Output - OrderCountOutput.cs

    [Function("orderCount", typeof(OrderCountOutput))]
    public class GetOrderCountFunction : FunctionMessage
    {
        
    }
}
