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
     * @dev Returns the amount of tokens in existence.
     */
    // function totalSupply() external view returns(uint256);
    // Output - TotalSupplyOutput.cs

    [Function("totalSupply", typeof(TotalSupplyOutput))]
    public class TotalSupplyFunction : FunctionMessage
    {
    }

}
