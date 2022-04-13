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
    // event Deposit(address tokenAddress, address userAddress, uint amount, uint balance);

    [Event("Deposit")]
    public class DepositEvent : IEventDTO
    {
        [Parameter("address", "tokenAddress", 1, false)]
        public string TokenAddress { get; set; }

        [Parameter("address", "userAddress", 2, false)]
        public string UserAddress { get; set; }

        [Parameter("uint256", "amount", 3, false)]
        public BigInteger Amount { get; set; }

        [Parameter("uint256", "balance", 4, false)]
        public BigInteger Balance { get; set; }
    }
}
