using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SchemaNote_A11085.Models
{
    public class AiritiTable
    {
        public string Column_Name { get; set; }
        public string Column_Description { get; set; }
        public bool Column_PK { get; set; }
        public string Column_Type { get; set; }
        public bool Column_IsNullable { get; set; }
        public string Column_Default { get; set; }
        public string Column_Remark { get; set; }        
    }
}
