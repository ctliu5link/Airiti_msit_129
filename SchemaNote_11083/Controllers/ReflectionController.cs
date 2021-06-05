using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchemaNote_11083.Models;
using SchemaNote_11083.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Controllers
{
    public class ReflectionController : Controller
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
                    SqlCommand commandTables = new SqlCommand(SQLCommand.dapper_getAllTables(), conn);
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(commandTables);
                    da.Fill(ds);

                    var tables = ds.Tables[0].DataMapping<Ttable_dapper>();
                    foreach(var table in tables)
                    {
                        SqlCommand command = new SqlCommand(SQLCommand.dapper_getTableColumns(), conn);
                        command.Parameters.Add(new SqlParameter("@Table_Name", table.Table_Name));
                        ds = new DataSet();
                        da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        table.TtableColumns = ds.Tables[0].DataMapping<TtableColumn>();

                    }
                    
                    return PartialView("DapperDBdata", new TtableSchema<IEnumerable<Ttable_dapper>>(tables));
                }
            }
            catch(Exception e )
            {
                Console.WriteLine(e);
            }
            return Content("error");
        }

    }
}
