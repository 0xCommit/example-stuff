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
    public class AllowanceOutput : IFunctionOutputDTO
    {
        [Parameter("uint256", "allowance", 1)]
        public BigInteger Allowance { get; set; }
    }
}
