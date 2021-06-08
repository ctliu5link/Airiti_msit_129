using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SchemaNote_A11085.Models
{
    public class a
    {
        public string Column_Name { get; set; }
        public string Column_Description { get; set; }
        public int Column_PK { get; set; }
        public string Column_Type { get; set; }
        public int Column_IsNullable { get; set; }
        public string Column_Default { get; set; }
        public string Column_Remark { get; set; }
       
    }
    public class b
    {
        public string TableName { get; set; }
        public string Main_UserTable { get; set; }
        //public string ObjectType { get; set; }
        public string DescriptionName { get; set; }
        public string Object_CreateDay { get; set; }
        public string Object_UpdateDay { get; set; }
        public string TotalCount { get; set; }
        public string Remark { get; set; }
    }

    public class Combine
    {
        public Combine() {
            a = new a();
            b = new b();
        }
            
        public a a { get; set; }
        public b b { get; set; }
        //public string Column_Name { get; set; }
        //public string Column_Description { get; set; }
        //public int Column_PK { get; set; }
        //public string Column_Type { get; set; }
        //public int Column_IsNullable { get; set; }
        //public string Column_Default { get; set; }
        //public string Column_Remark { get; set; }
        //public string TableName { get; set; }
        //public string Main_UserTable { get; set; }
        //public string ObjectType { get; set; }
        //public string DescriptionName { get; set; }
        //public string Object_CreateDay { get; set; }
        //public string Object_UpdateDay { get; set; }
        //public string TotalCount { get; set; }
        //public string Remark { get; set; }



    }
}


