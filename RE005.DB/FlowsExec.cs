using System;
using System.Collections.Generic;

#nullable disable

namespace RE005.DB
{
    public partial class FlowsExec
    {
        public FlowsExec()
        {
            FieldsByFlowExecs = new HashSet<FieldsByFlowExec>();
        }

        public int Id { get; set; }
        public int IdFlow { get; set; }
        public int? Sequence { get; set; }
        public DateTime RecordDate { get; set; }

        public virtual Flow IdFlowNavigation { get; set; }
        public virtual ICollection<FieldsByFlowExec> FieldsByFlowExecs { get; set; }
    }
}
