using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11086.Models
{
    public class UpdateExpendedProperty
    {
        public string table { get; set; }
        public string column { get; set; }
        public string target { get; set; }
        public string value { get; set; }

    }
}
