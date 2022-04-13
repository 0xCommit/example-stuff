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
    // function getEtherBalance(address _userAddress) public view returns (uint)
    // Gets the balance of Ether in the smart contract belonging to _userAddress

    // Output - EtherBalanceOutput.cs

    [Function("getEtherBalance", typeof(EtherBalanceOutput))]
    public class GetEtherBalanceFunction : FunctionMessage
    {
        [Parameter("address", "_userAddress", 1)]
        public string UserAddress { get; set; }
    }
}
