using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Models
{
    public class TtableColumn //需要的欄位內容資料
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
