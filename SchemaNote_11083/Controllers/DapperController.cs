using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchemaNote_11083.Models;
using SchemaNote_11083.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Controllers
{
    public class DapperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult DBdata(string DBConnection)
        {
            try
            { 
                using (var conn = new SqlConnection(DBConnection))
                {
                    var tables = conn.Query<Ttable_dapper>(SQLCommand.dapper_getAllTables());
                    foreach (var table in tables)
                    {
                        table.TtableColumns =
                            conn.Query<TtableColumn>(SQLCommand.dapper_getTableColumns(), new { Table_Name = table.Table_Name });
                    }

                    HttpContext.Session.SetString(CDictionary.Current_DBConnection, DBConnection);
                    ViewBag.ConnectionToken = CDictionary.Current_DBConnection;

                    return PartialView("DapperDBdata", new TtableSchema<IEnumerable<Ttable_dapper>>(tables));
                }
            }
            catch
            {

            }
            return Content("error");
        }

        [HttpPost]
        public void updateExpendedProperty([FromBody] tUpdateExpendedProperty data)
        {
            string dbconnection = HttpContext.Session.GetString(CDictionary.Current_DBConnection);

            using (SqlConnection conn = new SqlConnection(dbconnection))
            {
                try
                {
                    data.target = data.target == "Description" ? "MS_Description" : data.target;
                    string strcommand = data.column == "" ?
                        $"EXEC sys.sp_updateextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table" :
                        $"EXEC sys.sp_updateextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table, @level2type=N'COLUMN',@level2name=@data_column";
                    conn.Execute(strcommand, new
                    {
                        data_target = data.target,
                        data_value = data.value,
                        data_table = data.table,
                        data_column = data.column
                    });
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    //如果還未建立該擴充屬性，就改成新增擴充屬性
                    string strcommand = data.column == "" ?
                        $"EXEC sys.sp_addextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table" :
                        $"EXEC sys.sp_addextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table, @level2type=N'COLUMN',@level2name=@data_column";
                    conn.Execute(strcommand, new
                    {
                        data_target = data.target,
                        data_value = data.value,
                        data_table = data.table,
                        data_column = data.column
                    });
                }
            }
        }


    }
}
