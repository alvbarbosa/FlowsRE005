using System;
using System.Collections.Generic;

#nullable disable

namespace RE005.DB
{
    public partial class Flow
    {
        public Flow()
        {
            FlowsExecs = new HashSet<FlowsExec>();
            StepsByFlows = new HashSet<StepsByFlow>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Sequences { get; set; }

        public virtual ICollection<FlowsExec> FlowsExecs { get; set; }
        public virtual ICollection<StepsByFlow> StepsByFlows { get; set; }
    }
}
