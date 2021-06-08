using SchemaNote_A11085.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11085.Models 
{

    public class TableColumn
    {
        public string TableName { get; set; }
        public string Main_UserTable { get; set; }
        public string DescriptionName { get; set; }
        public string Object_CreateDay { get; set; }
        public string Object_UpdateDay { get; set; }
        public string TotalCount { get; set; }
        public string Remark { get; set; }
    }


}
