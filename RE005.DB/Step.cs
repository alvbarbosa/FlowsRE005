using System;
using System.Collections.Generic;

#nullable disable

namespace RE005.DB
{
    public partial class Step
    {
        public Step()
        {
            FieldsBySteps = new HashSet<FieldsByStep>();
            StepsByFlows = new HashSet<StepsByFlow>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Service { get; set; }

        public virtual ICollection<FieldsByStep> FieldsBySteps { get; set; }
        public virtual ICollection<StepsByFlow> StepsByFlows { get; set; }
    }
}
