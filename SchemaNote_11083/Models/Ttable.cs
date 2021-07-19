using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Models
{
    public class Ttable //需要的資料表資料內容
    {
        public string Table_Name { get; set; }
        public string Table_Schema { get; set; }
        public string Create_Date { get; set; }
        public string Modify_Date { get; set; }
        public long Total_Rows { get; set; }
        public string Table_Description { get; set; }
        public string Table_REMARK { get; set; }
        public IEnumerable<TtableColumn> TtableColumns { get; set; } //該資料表的欄位
    }
}
