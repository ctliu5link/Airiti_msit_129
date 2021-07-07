using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11086.Models
{
    public class TableColumn
    {
        public string Column_Name { get; set; }
        public int PK { get; set; }
        public int IS_Nullable { get; set; }
        public string Data_Type { get; set; }
        public string Column_Default { get; set; }
        public string Description { get; set; }
        public string REMARK { get; set; }
    }
}
