using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_A11087.Models
{
    public class SchemaNote
    {
        public string table_Name { get; set; }
        public string table_Schema { get; set; }
        public DateTime create_date { get; set; }
        public DateTime modify_date { get; set; }
        public string column_Name { get; set; }
        public string ms_Description { get; set; }
        public string data_Type { get; set; }
        public bool primary_Key { get; set; }
        public bool is_Nullable { get; set; }
        public string column_Default { get; set; }
        public string remark { get; set; }
       

    }
}
