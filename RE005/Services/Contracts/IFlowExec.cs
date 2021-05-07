using RE005.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RE005.Services.Contracts
{
    public interface IFlowExec
    {
        int sequence { get; }
        Task<ResponseFlow> ExecuteFlowAsync(RequestFlow data);
    }
}
