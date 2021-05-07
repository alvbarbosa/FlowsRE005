using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RE005.DB;
using RE005.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace RE005.Services
{
    internal class FlowsParams : IFlowsParams
    {
        public List<Flow> Flows { get; }
        

        public FlowsParams(IServiceScopeFactory factory)
        {
            RE005Context context = factory.CreateScope().ServiceProvider.GetRequiredService<RE005Context>();
            Flows = context.Flows
                .Include(a => a.StepsByFlows)
                .ThenInclude(a => a.IdStepNavigation)
                .ThenInclude(a => a.FieldsBySteps)
                .ThenInclude(a => a.IdFieldNavigation)
                .ToList();
        }

        public Flow GetFlow(string code)
        {
            Flow flow = Flows.Where(a => a.Code == code).FirstOrDefault();

            return flow;
        }
    }
}
