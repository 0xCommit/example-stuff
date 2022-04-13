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
    // depositEther wrapper in Nethereum C# not implemented yet.

    [Function("depositEther", typeof(DepositEtherOutput))]
    public class DepositEtherFunction : FunctionMessage
    { 
        public DepositEtherFunction()
        {
            throw new NotImplementedException();
        }
    }
}
