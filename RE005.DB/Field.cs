using System;
using System.Collections.Generic;

#nullable disable

namespace RE005.DB
{
    public partial class Field
    {
        public Field()
        {
            FieldsByFlowExecs = new HashSet<FieldsByFlowExec>();
            FieldsBySteps = new HashSet<FieldsByStep>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public virtual ICollection<FieldsByFlowExec> FieldsByFlowExecs { get; set; }
        public virtual ICollection<FieldsByStep> FieldsBySteps { get; set; }
    }
}
