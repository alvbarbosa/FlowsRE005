using System;
using System.Collections.Generic;

#nullable disable

namespace RE005.DB
{
    public partial class StepsByFlow
    {
        public int Id { get; set; }
        public int IdFlow { get; set; }
        public int IdStep { get; set; }
        public int Sequence { get; set; }

        public virtual Flow IdFlowNavigation { get; set; }
        public virtual Step IdStepNavigation { get; set; }
    }
}
