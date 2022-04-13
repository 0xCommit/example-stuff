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

using NethereumTest.Exchange.Outputs;

namespace NethereumTest.Exchange.Functions
{
    // function getTokenBalance(address _tokenAddress, address _userAddress) public view returns (uint)
    // Gets the balance of a token specified by _tokenAddress in the exchange smart contract belonging to _userAddress

    // Output - TokenBalanceOutput.cs

    [Function("getTokenBalance", typeof(TokenBalanceOutput))]
    public class GetTokenBalanceFunction : FunctionMessage
    {
        [Parameter("address", "_tokenAddress", 1)]
        public string TokenAddress { get; set; }

        [Parameter("address", "_userAddress", 2)]
        public string UserAddress { get; set; }
    }
}
