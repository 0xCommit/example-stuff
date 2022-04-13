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
    // function createOrder(address _tokenReceive, uint _amountReceive, address _tokenGive, uint _amountGive) public {
    // Creates an order to buy _tokenReceive and sell _tokenGive, with the specified amounts/price.

    // Output - CreateOrderOutput.cs

    [Function("createOrder", typeof(CreateOrderOutput))]
    public class CreateOrderFunction : FunctionMessage
    {
        [Parameter("address", "_tokenReceive", 1)]
        public string TokenReceiveAddress { get; set; }

        [Parameter("uint256", "_amountReceive", 2)]
        public BigInteger AmountReceive { get; set; }

        [Parameter("address", "_tokenGive", 3)]
        public string TokenGiveAddress { get; set; }

        [Parameter("uint256", "_amountGive", 4)]
        public BigInteger AmountGive { get; set; }
    }
}
