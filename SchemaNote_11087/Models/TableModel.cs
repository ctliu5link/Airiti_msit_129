using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_A11087.Models
{
    public class TableModel
    {       
        [DisplayName("欄位名稱")]
        public string ColumnName { get; set; }
        [DisplayName("欄位說明")]
        public string MSDescription { get; set; }
        [DisplayName("資料型態")]
        public string DataType { get; set; }
        public string CharacterMaximumLength { get; set; }
        [DisplayName("主鍵")]
        public int IsPK { get; set; }
        [DisplayName("不為Null")]

        public string IsNullable { get; set; }
        [DisplayName("預設值")]

        public string ColumnDefault { get; set; }
        [DisplayName("備註")]

        public string Remark { get; set; }
        [DisplayName("結構描述名稱")]

        public string TableSchema { get; set; }

        [DisplayName("物件創建日期")]
        public string CreateDate { get; set; }
        [DisplayName("物件修改日期")]

        public string ModifyDate { get; set; }
        public int TotalRows { get; set; } //資料總筆數
        public string TableName { get; set; }
        public string TableMSDescription { get; set; }
        public string TableRemark { get; set; }
    }
}
