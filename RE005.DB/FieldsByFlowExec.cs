using System;
using System.Collections.Generic;

#nullable disable

namespace RE005.DB
{
    public partial class FieldsByFlowExec
    {
        public int Id { get; set; }
        public int IdFlowExec { get; set; }
        public int IdField { get; set; }
        public string Value { get; set; }
        public DateTime RecordDate { get; set; }

        public virtual Field IdFieldNavigation { get; set; }
        public virtual FlowsExec IdFlowExecNavigation { get; set; }
    }
}
