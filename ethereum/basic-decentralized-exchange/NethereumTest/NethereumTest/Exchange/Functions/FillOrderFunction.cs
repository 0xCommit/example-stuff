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
    // function fillOrder(uint _id) public
    // This function is called by the trade _taker_, not creator. Executes against the order specified with id and fills the order. Calls internal _trade function to move balances around.

    // Output - FillOrderOutput.cs

    [Function("fillOrder", typeof(FillOrderOutput))]
    public class FillOrderFunction : FunctionMessage
    {
        [Parameter("uint256", "id", 1)]
        public BigInteger OrderId { get; set; }

    }
}
