using System;
using System.Collections.Generic;

#nullable disable

namespace RE005.DB
{
    public partial class FieldsByStep
    {
        public int Id { get; set; }
        public int IdStep { get; set; }
        public int IdField { get; set; }
        public string Type { get; set; }

        public virtual Field IdFieldNavigation { get; set; }
        public virtual Step IdStepNavigation { get; set; }
    }
}
