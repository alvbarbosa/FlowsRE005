using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RE005.Models
{
    public class RequestFlow
    {
        [Required]
        public string Flow { get; set; }
        public int Id { get; set; }
        public Dictionary<string,string> Fields { get; set; }
    }
}
