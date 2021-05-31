using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SchemaNote_A11087.Models;

namespace SchemaNote_A11087.ViewModel
{
    public class SchemaNoteViewModel
    {
        private SchemaNote iv_schemaNote = null;

        public SchemaNote schemaNote { get { return iv_schemaNote; } }

        public SchemaNoteViewModel(SchemaNote p)
        {
            iv_schemaNote = p;
        }
        public SchemaNoteViewModel()
        {
            iv_schemaNote = new SchemaNote();
        }
        [DisplayName("資料表名稱")]
        public string table_Name { get { return iv_schemaNote.table_Name; } set { iv_schemaNote.table_Name = value; } }

        [DisplayName("結構描述名稱")]
        public string table_Schema { get { return iv_schemaNote.table_Schema; } set { iv_schemaNote.table_Schema = value; } }

        [DisplayName("物件創建日期")]
        public DateTime create_date { get { return iv_schemaNote.create_date; } set { iv_schemaNote.create_date = value; } }
        [DisplayName("物件修改日期")]
        public DateTime modify_date { get { return iv_schemaNote.modify_date; } set { iv_schemaNote.modify_date = value; } }

        [DisplayName("欄位名稱")]
        public string column_Name { get { return iv_schemaNote.column_Name; } set { iv_schemaNote.column_Name = value; } }

        [DisplayName("欄位說明")]
        public string ms_Description { get { return iv_schemaNote.ms_Description; } set { iv_schemaNote.ms_Description = value; } }
        
        [DisplayName("資料型態")]
        public string data_Type { get { return iv_schemaNote.data_Type; } set { iv_schemaNote.data_Type = value; } }

        [DisplayName("主鍵")]
        public bool primary_Key { get { return iv_schemaNote.primary_Key; } set { iv_schemaNote.primary_Key = value; } }

        [DisplayName("不為Null")]
        public bool is_Nullable { get { return iv_schemaNote.is_Nullable; } set { iv_schemaNote.is_Nullable = value; } }
        
        [DisplayName("預設值")]
        public string column_Default { get { return iv_schemaNote.column_Default; } set { iv_schemaNote.column_Default = value; } }
        
        [DisplayName("備註")]
        public string remark { get { return iv_schemaNote.remark; } set { iv_schemaNote.remark = value; } }



    }
}
