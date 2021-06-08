using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchemaNote_11083.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using SchemaNote_11083.ViewModels;
using Microsoft.AspNetCore.Http;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace SchemaNote_11083.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration p_configuration )
        {
            _logger = logger;
            _configuration = p_configuration;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult schemaNote()
        {
            return View();
        }



        public ActionResult DBdata(string DBConnection)
        {
            Ttable[] Tables = null;

            SqlConnection conn = null;

            try
            {
                using (conn = new SqlConnection(DBConnection))
                {
                    var adapter = new SqlDataAdapter(SQLCommand.schemaCommand(), conn);
                    var dataset = new DataSet();
                    adapter.Fill(dataset);

                    var dtRows = dataset.Tables[0]; //所有欄位資料
                    var dtTable = dataset.Tables[1]; //所有table資料

                    Tables = new Ttable[dtTable.Rows.Count];
                    int tableCount = 0;

                    foreach (DataRow item in dtTable.Rows)
                    {
                        Ttable newTable = new Ttable
                        {
                            Table_Name = item["Table_Name"].ToString(),
                            TtableColumns = new TtableColumn[(int)item["Column_qty"]]
                        };

                        Tables[tableCount] = newTable;
                        tableCount++;
                    }

                    Ttable currentTable = null;

                    int columnCount = 0;
                    foreach (DataRow item in dataset.Tables[0].Rows)
                    {
                        if (currentTable == null || item["Table_Name"].ToString() != currentTable.Table_Name)
                        {
                            columnCount = 0;
                            currentTable = Tables.FirstOrDefault(n => n.Table_Name == item["Table_Name"].ToString());
                            currentTable.Table_Description = item["Table_Description"].ToString();
                            currentTable.Table_REMARK = item["Table_REMARK"].ToString();
                            currentTable.Table_Schema = item["Table_Schema"].ToString();
                            currentTable.Create_Date = item["Create_Date"].ToString();
                            currentTable.Modify_Date = item["Modify_Date"].ToString();
                            currentTable.Total_Rows = (long)item["Total_Rows"];
                        }

                        TtableColumn newColumn = new TtableColumn
                        {
                            Column_Default = item["Column_Default"].ToString(),
                            Column_Name = item["Column_Name"].ToString(),
                            Data_Type = item["Data_Type"].ToString(),
                            Description = item["Description"].ToString(),
                            IS_Nullable = item["IS_Nullable"].ToString() == "YES" ? 1 : 0,
                            PK = item["PK"].ToString() == "" ? 0 : 1,
                            REMARK = item["REMARK"].ToString()
                        };
                        currentTable.TtableColumns[columnCount] = newColumn;
                        columnCount++;
                    }
                    HttpContext.Session.SetString(CDictionary.Current_DBConnection, DBConnection);
                    ViewBag.ConnectionToken = CDictionary.Current_DBConnection;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Content("error");
            }
            finally
            {
                conn.Close();
            }

            return PartialView("DBdata", new TtableSchema<Ttable[]>(Tables));
            //return View(new TtableSchema(Tables));

        }

        [HttpPost]
        public void updateExpendedProperty([FromBody] tUpdateExpendedProperty data)
        {
            string dbconnection = HttpContext.Session.GetString(CDictionary.Current_DBConnection);

            using (SqlConnection conn = new SqlConnection(dbconnection))
            {
                try
                {
                    conn.Open();
                    data.target = data.target == "Description" ? "MS_Description" : data.target;
                    string strcommand = data.column == "" ?
                        $"EXEC sys.sp_updateextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table" :
                        $"EXEC sys.sp_updateextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table, @level2type=N'COLUMN',@level2name=@data_column";
                    SqlCommand command = new SqlCommand(strcommand,conn);
                    command.Parameters.Add(new SqlParameter("@data_target", data.target));
                    command.Parameters.Add(new SqlParameter("@data_value", data.value));
                    command.Parameters.Add(new SqlParameter("@data_table", data.table));
                    command.Parameters.Add(new SqlParameter("@data_column", data.column));

                    command.ExecuteNonQuery();
                    
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    //如果還未建立該擴充屬性，就改成新增擴充屬性
                    string strcommand = data.column == "" ?
                        $"EXEC sys.sp_addextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table" :
                        $"EXEC sys.sp_addextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table, @level2type=N'COLUMN',@level2name=@data_column";
                    SqlCommand command = new SqlCommand(strcommand, conn);
                    command.Parameters.Add(new SqlParameter("@data_target", data.target));
                    command.Parameters.Add(new SqlParameter("@data_value", data.value));
                    command.Parameters.Add(new SqlParameter("@data_table", data.table));
                    command.Parameters.Add(new SqlParameter("@data_column", data.column));

                    command.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public ActionResult keepSession(string token)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(token)))
                return Content("lost");
            return Content("ok");
        }



       
    }


}
