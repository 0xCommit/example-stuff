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
     * @dev Moves `amount` tokens from `sender` to `recipient` using the
     * allowance mechanism. `amount` is then deducted from the caller's
     * allowance.
     *
     * Returns a boolean value indicating whether the operation succeeded.
     *
     * Emits a {Transfer} event.
     */
    // function transferFrom(address sender, address recipient, uint256 amount) external returns(bool);
    // Output - no output class

    [Function("transferFrom", "bool")]
    public class TransferFromFunction : FunctionMessage
    {
        [Parameter("address", "sender", 1)]
        public string Sender { get; set; }

        [Parameter("address", "recipient", 2)]
        public string Recipient { get; set; }

        [Parameter("uint256", "amount", 3)]
        public BigInteger Amount { get; set; }
    }
}
