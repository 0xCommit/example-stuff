using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace NethereumTest.ERC20.Functions
{
    /**
     * @dev Returns the remaining number of tokens that `spender` will be
     * allowed to spend on behalf of `owner` through {transferFrom}. This is
     * zero by default.
     *
     * This value changes when {approve} or {transferFrom} are called.
     */
    // function allowance(address owner, address spender) external view returns (uint256);
    // Output - AllowanceOutput.cs

    [Function("allowance", "uint256")]
    public class AllowanceFunction : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public string Owner { get; set; }

        [Parameter("address", "spender", 2)]
        public string Spender { get; set; }
    }
}
