﻿using System;
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
    public class FilledOrderOutput : IFunctionOutputDTO
    {
        [Parameter("bool", 1)]
        public bool Filled { get; set; }
    }
}
