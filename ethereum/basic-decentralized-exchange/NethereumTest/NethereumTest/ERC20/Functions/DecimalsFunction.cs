using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum;
using Nethereum.Web3;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;

using NethereumTest.ERC20.Outputs;

namespace NethereumTest.ERC20.Functions
{
    /**
     * @dev Returns the decimals places of the token.
     */
    // function decimals() external view returns (uint8);
    // Output - DecimalsOutput.cs

    [Function("decimals", typeof(DecimalsOutput))]
    public class DecimalsFunction : FunctionMessage
    {
    }

}
