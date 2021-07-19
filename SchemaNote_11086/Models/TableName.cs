using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11086.Models
{
    public class TableName
    {
        public string Table_Name { get; set; }
        public string Table_Schema { get; set; }
        public string Create_Date { get; set; }
        public string Modify_Date { get; set; }
        public long Total_Rows { get; set; }
        //說明
        public string Table_Description { get; set; }
        //備註
        public string Table_REMARK { get; set; }
        public TableColumn[] TtableColumns { get; set; }
    }
}
