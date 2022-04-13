using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace NethereumTest.Exchange.Outputs
{
    [FunctionOutput]
    public class Order : IFunctionOutputDTO
    {
        [Parameter("uint256", "id", 1)]
        public BigInteger Id { get; set; }

        [Parameter("address", "creator", 2)]
        public string Creator { get; set; }

        [Parameter("address", "tokenAddressReceive", 3)]
        public string TokenAddressReceive { get; set; }

        [Parameter("address", "tokenAddressGive", 4)]
        public string TokenAddressGive { get; set; }

        [Parameter("uint256", "amountReceive", 5)]
        public BigInteger AmountReceive { get; set; }

        [Parameter("uint256", "amountGive", 6)]
        public BigInteger AmountGive { get; set; }

        [Parameter("uint256", "timestamp", 7)]
        public BigInteger Timestamp { get; set; }
    }
}
