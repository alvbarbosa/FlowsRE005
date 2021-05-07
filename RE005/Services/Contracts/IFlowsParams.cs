using RE005.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RE005.Services.Contracts
{
    interface IFlowsParams
    {
        List<Flow> Flows { get; }
        Flow GetFlow(string code);
    }
}
