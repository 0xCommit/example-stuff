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
     * @dev Atomically decreases the allowance granted to `spender` by the caller.
     *
     * This is an alternative to {approve} that can be used as a mitigation for
     * problems described in {IERC20-approve}.
     *
     * Emits an {Approval} event indicating the updated allowance.
     *
     * Requirements:
     *
     * - `spender` cannot be the zero address.
     * - `spender` must have allowance for the caller of at least
     * `subtractedValue`.
     */
    // function decreaseAllowance(address spender, uint256 subtractedValue) public virtual returns (bool) ;
    // Output - no output class

    [Function("decreaseAllowance", "bool")]
    public class DecreaseAllowance : FunctionMessage
    {
        [Parameter("address", "spender", 1)]
        public string Spender { get; set; }

        [Parameter("uint256", "subtractedValue", 2)]
        public BigInteger SubtractedValueAmount { get; set; }
    }
}
