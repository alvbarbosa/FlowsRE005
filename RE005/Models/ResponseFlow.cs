using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RE005.Models
{
    public class ResponseFlow
    {
        public int Id { get; set; }
        public Dictionary<string,string> Fields { get; set; }
    }
}
