using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_A11085.ViewModel
{
    public class ScheNoteViewModel
    {
        //建構子
        public ScheNoteViewModel()
        {
            //初始化時，給予SchemaNote預設值，讓他不要是null
            SchemaNote = new List<CAccountViewModel>();
        }
        public List<CAccountViewModel> SchemaNote { get; set; }
    }
}
