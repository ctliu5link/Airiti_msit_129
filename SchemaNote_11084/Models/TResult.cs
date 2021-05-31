using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11084.Models
{
    public class TResult
    {
        public string Table_Name { get; set; }
        public string COLUMN_NAME { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string 資料型態 { get; set; }
        public string 主鍵 { get; set; }
        public string 預設值 { get; set; }
        public string 欄位說明 { get; set; }
        public string 備註 { get; set; }
    }
}
