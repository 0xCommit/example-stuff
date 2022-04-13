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
    public class NameOutput : IFunctionOutputDTO
    {
        [Parameter("string", "name", 1)]
        public string Name { get; set; }
    }
}
