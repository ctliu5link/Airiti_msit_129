using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SchemaNote_A11085.Models
{
    public class AiritiTable
    {
        public string Table_Name { get; set; }
        public string Table_Description { get; set; }
        public string Table_PK { get; set; }
        public string Table_Type { get; set; }
        public string Table_IsNullable { get; set; }
        public string Table_Default { get; set; }
        public string Table_Remark { get; set; }        
    }
}
