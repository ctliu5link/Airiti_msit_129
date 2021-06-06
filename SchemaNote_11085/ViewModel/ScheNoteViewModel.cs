using SchemaNote_11085.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_A11085.ViewModel
{
    public class ScheNoteViewModel
    {
        private TableColumn iv_airititable = null;
        public TableColumn schairitable { get { return iv_airititable; } }

        public ScheNoteViewModel(TableColumn a)
        {
            iv_airititable = a;
        }
        public ScheNoteViewModel()
        {
            iv_airititable = new TableColumn();
        }
        public string TableName { get; set; }
        public string Main_UserTable { get; set; }
        public string ObjectType { get; set; }
        public string DescriptionName { get; set; }
        public string Object_CreateDay { get; set; }
        public string Object_UpdateDay { get; set; }
        public string TotalCount { get; set; }
        public string Remark { get; set; }
    }
}
