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
     * @dev Returns the amount of tokens owned by `account`.
     */
    // function balanceOf(address account) external view returns(uint256);
    // Output - BalanceOfOutputDTO.cs

    [Function("balanceOf", typeof(BalanceOfOutput))]
    public class BalanceOfFunction : FunctionMessage
    {
        [Parameter("address", "account", 1)]
        public string Account { get; set; }
    }
}
