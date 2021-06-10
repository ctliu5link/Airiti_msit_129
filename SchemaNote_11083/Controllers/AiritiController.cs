using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airiti.Common;
using Airiti.DataAccess;
using SchemaNote_11083.Models;
using SchemaNote_11083.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

namespace SchemaNote_11083.Controllers
{
    public class AiritiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult DBdata(string DBConnection)
        {
            try
            {
                var tables = DBService.SingleQuery(DBConnection, SQLCommand.dapper_getAllTables())
                    .ReturnData.DataMapping<Ttable_dapper>();
                foreach(var table in tables)
                {
                    table.TtableColumns = DBService.SingleQuery(
                        DBConnection, 
                        SQLCommand.dapper_getTableColumns(), 
                        new List<QueryField>
                    {
                        new QueryField("@Table_Name",table.Table_Name)
                    }).ReturnData.DataMapping<TtableColumn>();
                }
                HttpContext.Session.SetString(CDictionary.Current_DBConnection, DBConnection);
                ViewBag.ConnectionToken = CDictionary.Current_DBConnection;

                return PartialView("AiritiDBdata", new TtableSchema<IEnumerable<Ttable_dapper>>(tables));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Content("error");
        }

        [HttpPost]
        public void updateExpendedProperty([FromBody] tUpdateExpendedProperty data)
        {
            string dbconnection = HttpContext.Session.GetString(CDictionary.Current_DBConnection);

                try
                {
                    data.target = data.target == "Description" ? "MS_Description" : data.target;
                    string strcommand = data.column == "" ?
                        $"EXEC sys.sp_updateextendedproperty @name={data.target}, @value={data.value} , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name={data.table}" :
                        $"EXEC sys.sp_updateextendedproperty @name={data.target}, @value={data.value} , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name={data.table}, @level2type=N'COLUMN',@level2name={data.column}";
                    DBService.MutiNonQuery(dbconnection, strcommand);
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    //如果還未建立該擴充屬性，就改成新增擴充屬性
                    string strcommand = data.column == "" ?
                        $"EXEC sys.sp_addextendedproperty @name={data.target}, @value={data.value} , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name={data.table}" :
                        $"EXEC sys.sp_addextendedproperty @name={data.target}, @value={data.value} , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name={data.table}, @level2type=N'COLUMN',@level2name={data.column}";
                DBService.MutiNonQuery(dbconnection, strcommand);
            }
            }
        }
    }
}
