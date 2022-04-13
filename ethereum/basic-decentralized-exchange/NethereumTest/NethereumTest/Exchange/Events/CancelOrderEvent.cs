using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace NethereumTest.Exchange.Events
{
    // event CancelOrder(uint id, address user, address tokenReceive, uint amountReceive, address tokenGive, uint amountGive, uint timestamp);

    [Event("CancelOrder")]
    public class CancelOrderEvent : IEventDTO
    {
        [Parameter("uint256", "id", 1, false)]
        public BigInteger Id { get; set; }

        [Parameter("address", "user", 2, false)]
        public string UserAddress { get; set; }

        [Parameter("address", "tokenReceive", 3, false)]
        public string TokenReceiveAddress { get; set; }

        [Parameter("uint256", "amountReceive", 4, false)]
        public BigInteger AmountReceive { get; set; }

        [Parameter("address", "tokenGive", 5, false)]
        public string TokenGiveAddress { get; set; }

        [Parameter("uint256", "amountGive", 6, false)]
        public BigInteger AmountGive { get; set; }

        [Parameter("uint256", "timestamp", 7, false)]
        public BigInteger Timestamp { get; set; }



    }
}
