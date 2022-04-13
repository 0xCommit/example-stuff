using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace NethereumTest.ERC20.Events
{
    // event Approval(address indexed owner, address indexed spender, uint256 value);

    [Event("Approval")]
    public class ApprovalEvent : IEventDTO
    {
        [Parameter("address", "owner", 1, true)]
        public string Owner { get; set; }

        [Parameter("address", "spender", 2, true)]
        public string Spender { get; set; }

        [Parameter("uint256", "value", 3, false)]
        public BigInteger Value { get; set; }
    }
}
