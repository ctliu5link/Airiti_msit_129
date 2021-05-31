using SchemaNote_11086.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11086.ViewModels
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
        public string tableName { get { return iv_schemaNote.tableName; } set { iv_schemaNote.tableName = value; } }
        public string t表備註 { get { return iv_schemaNote.t表備註; } set { iv_schemaNote.t表備註 = value; } }
        public string t表說明 { get { return iv_schemaNote.t表說明; } set { iv_schemaNote.t表說明 = value; } }
        public string t結構描述 { get { return iv_schemaNote.t結構描述; } set { iv_schemaNote.t結構描述 = value; } }
        public string t創建時間 { get { return iv_schemaNote.t創建時間; } set { iv_schemaNote.t創建時間 = value; } }
        public string t修改時間 { get { return iv_schemaNote.t修改時間; } set { iv_schemaNote.t修改時間 = value; } }
        public string columnName { get { return iv_schemaNote.columnName; } set { iv_schemaNote.columnName = value; } }
        public string c欄位說明 { get { return iv_schemaNote.c欄位說明; } set { iv_schemaNote.c欄位說明 = value; } }
        public string c欄位備註 { get { return iv_schemaNote.c欄位備註; } set { iv_schemaNote.c欄位備註 = value; } }
        public string c資料型別 { get { return iv_schemaNote.c資料型別; } set { iv_schemaNote.c資料型別 = value; } }
        public string c主鍵 { get { return iv_schemaNote.c主鍵; } set { iv_schemaNote.c主鍵 = value; } }
        public string c不為Null { get { return iv_schemaNote.c不為Null; } set { iv_schemaNote.c不為Null = value; } }
        public string c預設值 { get { return iv_schemaNote.c預設值; } set { iv_schemaNote.c預設值 = value; } }


    }
}
