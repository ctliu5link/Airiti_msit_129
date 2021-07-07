using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11086.Models
{
    public class SchemaNote
    {
        public string tableName{get;set;}
        public string t表備註 { get; set; }
        public string t表說明 { get; set; }
        public string t結構描述 { get; set; }
        public string t創建時間 { get; set; }
        public string t修改時間 { get; set; }
        public string columnName { get; set; }
        public string c欄位說明 { get; set; }
        public string c欄位備註 { get; set; }
        public string c資料型別 { get; set; }
        public string c主鍵 { get; set; }
        public string c不為Null { get; set; }
        public string c預設值 { get; set; }
        

    }
}
