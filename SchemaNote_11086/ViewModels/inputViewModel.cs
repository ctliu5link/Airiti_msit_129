using SchemaNote_11086.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11086.ViewModels
{
    public class inputViewModel
    {
        public List<SchemaNoteViewModel> show { get; set; }
        public List<SchemaNoteViewModel> showTableNAme { get; set; }

        public List<TableName> tableNames { get; set; }
        public List<TableColumn> tableColumns { get; set; }       



    }
}
