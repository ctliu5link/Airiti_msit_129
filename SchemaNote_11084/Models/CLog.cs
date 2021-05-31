using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11084.Models
{
    public class CLog
    {
        public string time { get; set; }
        public string editor { get; set; }
                
        public string message { get; set; }
    }
}
