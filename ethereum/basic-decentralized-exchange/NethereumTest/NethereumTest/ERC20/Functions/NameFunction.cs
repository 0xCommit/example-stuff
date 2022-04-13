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
     * @dev Returns the name of the token.
     */
    // function name() external view returns (string memory);
    // Output - NameOutput

    [Function("name", typeof(NameOutput))]
    public class NameFunction : FunctionMessage
    {
    }

}
