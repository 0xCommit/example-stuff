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

namespace NethereumTest.ERC20.Functions
{
    /**
     * @dev Sets `amount` as the allowance of `spender` over the caller's tokens.
     *
     * Returns a boolean value indicating whether the operation succeeded.
     *
     * Emits an {Approval} event.
     */
    // function approve(address spender, uint256 amount) external returns (bool);
    // Output - no output class

    [Function("approve", "bool")]
    public class ApproveFunction : FunctionMessage
    {
        [Parameter("address", "spender", 1)]
        public string Spender { get; set; }

        [Parameter("uint256", "amount", 2)]
        public BigInteger Amount { get; set; }
    }
}
