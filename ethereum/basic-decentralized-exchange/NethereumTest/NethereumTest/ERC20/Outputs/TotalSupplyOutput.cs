using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace NethereumTest.ERC20.Outputs
{
    [FunctionOutput]
    public class TotalSupplyOutput : IFunctionOutputDTO
    {
        [Parameter("uint256", "supply", 1)]
        public BigInteger Supply { get; set; }
    }
}
