using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchemaNote_A11087.Models;
using System.Data.SqlClient;
using System.Data;
using SchemaNote_A11087.ViewModel;

namespace SchemaNote_A11087.Controllers
{
    public class AccountController : Controller
    {
        AiritiCheckContext db = new AiritiCheckContext();
        public IActionResult Index()
        {
            DataSet ds = new DataSet();
            string con = @"Data Source = DESKTOP-13I52L2\SQLEXPRESS; Initial Catalog = AiritiCheck; Integrated Security = True";   
            string comm = @"select o.TABLE_NAME,o.COLUMN_NAME,o.TABLE_SCHEMA,DATA_TYPE +'('+cast(CHARACTER_MAXIMUM_LENGTH as nvarchar)+')'as 資料型態,k.type as 主鍵,IS_NULLABLE as 不為Null,COLUMN_DEFAULT as 預設值, m.value as 欄位說明,r.value as 備註,
                    FORMAT(d.create_date, 'd', 'zh-cn' ) 'create_date', FORMAT(d.modify_date, 'd', 'zh-cn' ) 'modify_date'
                    FROM INFORMATION_SCHEMA.COLUMNS as o
                    LEFT JOIN sys.tables as d
                    on d.name=o.TABLE_NAME COLLATE SQL_Latin1_General_CP1_CI_AS
                    LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE as pk on pk.COLUMN_NAME=o.COLUMN_NAME
                    --AND pk.CONSTRAINT_CATALOG = o.TABLE_CATALOG
                    --AND pk.TABLE_NAME=o.TABLE_NAME
                    LEFT JOIN sys.key_constraints as k 
                    on k.name=pk.CONSTRAINT_NAME
                    LEFT JOIN fn_listextendedproperty(NULL, 'user', 'dbo', 'table', 'Account', 'column',default) AS m
                    on m.name='MS_Description' and o.COLUMN_NAME = m.objname COLLATE SQL_Latin1_General_CP1_CI_AS 
                    LEFT JOIN fn_listextendedproperty(NULL, 'user', 'dbo', 'table', 'Account', 'column',default) AS r
                    on r.name='REMARK'and o.COLUMN_NAME = r.objname COLLATE SQL_Latin1_General_CP1_CI_AS 
                    WHERE o.TABLE_NAME = 'Account'";

            SqlConnection sql = new SqlConnection(con);

            SqlCommand command = new SqlCommand($"{comm}", sql);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            
            dataAdapter.Fill(ds);

            List<SchemaNoteViewModel> list = new List<SchemaNoteViewModel>();
            
            foreach(DataRow t in ds.Tables[0].Rows)
            {
                SchemaNote schemaNote = new SchemaNote();
                schemaNote.table_Name = t["TABLE_NAME"].ToString();

                list.Add(new SchemaNoteViewModel(schemaNote));
            }
            
                  
            //var accounts = db.Accounts.ToList();  
            return View(list);
        }
        public IActionResult Edit()
        {
            return View();
        }

        //static void Main()
        //{
        //    string connectionString = @"Data Source = DESKTOP - 13I52L2\SQLEXPRESS; Initial Catalog = AiritiCheck; Integrated Security = True";
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {               
        //        conn.Open();
        //        DataTable table = conn.GetSchema("Tables");

        //    }
        //}
    }
}
