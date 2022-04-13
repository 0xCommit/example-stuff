using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace NethereumTest.Exchange.Outputs
{
    [FunctionOutput]
    public class DepositEtherOutput : IFunctionOutputDTO
    {
        // depositEther function not yet implemented in this wrapper.

        public DepositEtherOutput()
        {
            throw new NotImplementedException();
        }
    }
}
