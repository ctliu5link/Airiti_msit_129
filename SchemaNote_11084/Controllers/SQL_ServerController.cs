using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SchemaNote_11084.Connection;
using SchemaNote_11084.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Text;
using SchemaNote_11084.ViewModel;
using Microsoft.Extensions.Logging;
using SchemaNote_11084.MyClass;

namespace SchemaNote_11084.Controllers
{
    public class SQL_ServerController : FilterController
    {
        private ILogger logger;
        
        private readonly IConfiguration _configuration;
        public SQL_ServerController(ILogger<SQL_ServerController> log, IConfiguration configuration)
        {
            logger = log;
            _configuration = configuration;
        }
        
        public IActionResult Index()
        {            
            ViewBag.lisTableName = mGetTableName();
            List<TResult> resultT;
            List<TResult> result;
            
            // 預設表為 Account
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                //result = conn.Query<TResult>("dbo.spGetall", null, commandType: System.Data.CommandType.StoredProcedure).ToList();              
                DynamicParameters pmT = new DynamicParameters();
                pmT.Add("@TName", "Account", dbType: System.Data.DbType.String, System.Data.ParameterDirection.Input);
                resultT = conn.Query<TResult>("dbo.spTable", pmT, commandType: System.Data.CommandType.StoredProcedure).ToList();

                DynamicParameters pm = new DynamicParameters();
                pm.Add("@TName", "Account", dbType: System.Data.DbType.String, System.Data.ParameterDirection.Input);
                result = conn.Query<TResult>("dbo.spMy", pm, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            List<List<TResult>> a = new List<List<TResult>>();
            a.Add(resultT);
            a.Add(result);

            return View(a);
        }

        /// <summary>
        /// 取得所有資列表名稱
        /// </summary>
        /// <returns>List&lt;string&gt;</returns>
        private List<string> mGetTableName()
        {
            List<string> list= new List<string>();
            IEnumerable<dynamic> results = null;

            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string comm = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";                
                results = conn.Query<dynamic>(comm).ToList();
            }

            foreach (var row in results)
            {
                var fields = row as IDictionary<string, object>;
                list.Add(fields["TABLE_NAME"].ToString());
            }

            return list;
        }
                
        public IActionResult Filter(string cate)
        {
            List<TResult> resultT;
            List<TResult> result;

            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters pmT = new DynamicParameters();
                pmT.Add("@TName", cate, dbType: System.Data.DbType.String, System.Data.ParameterDirection.Input);
                resultT = conn.Query<TResult>("dbo.spTable", pmT, commandType: System.Data.CommandType.StoredProcedure).ToList();

                DynamicParameters pm = new DynamicParameters();
                pm.Add("@TName", cate, dbType: System.Data.DbType.String, System.Data.ParameterDirection.Input);
                result = conn.Query<TResult>("dbo.spMy", pm, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            List<List<TResult>> a = new List<List<TResult>>();
            a.Add(resultT);
            a.Add(result);

            return PartialView("Filter", a);
        }

        public IActionResult Edit(string Id, string Column)
        {
            ViewBag.lisTableName = mGetTableName();
            
            if (Id != null && Id != "All")
            {
                TResult result;
                using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    DynamicParameters pm = new DynamicParameters();
                    pm.Add("@TName", Id, dbType: System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    result = conn.Query<TResult>("dbo.spMy", pm, commandType: System.Data.CommandType.StoredProcedure).Where(n => n.COLUMN_NAME == Column).FirstOrDefault();
                }
                result.Table_Name = Id;

                HttpContext.Session.SetObject<TResult>(DDictionary.Result_Old, result);

                return PartialView("Edit", result);
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Edit(TResult r)
        {
            if (r != null)
            {                
                using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    try
                    {
                        conn.Execute("sp_updateextendedproperty", new
                        {
                            name = "MS_Description",
                            level0type = "SCHEMA",
                            level0name = "dbo",
                            level1type = "TABLE",
                            level1name = r.Table_Name,
                            level2type = "COLUMN",
                            level2name = r.COLUMN_NAME,
                            value = r.欄位說明
                        }, commandType: System.Data.CommandType.StoredProcedure);
                    }
                    catch (SqlException e)
                    {
                        conn.Execute("sp_addextendedproperty", new
                        {
                            name = "MS_Description",
                            level0type = "SCHEMA",
                            level0name = "dbo",
                            level1type = "TABLE",
                            level1name = r.Table_Name,
                            level2type = "COLUMN",
                            level2name = r.COLUMN_NAME,
                            value = r.欄位說明
                        }, commandType: System.Data.CommandType.StoredProcedure);
                    }

                    try
                    {
                        conn.Execute("sp_updateextendedproperty", new
                        {
                            name = "REMARK",
                            level0type = "SCHEMA",
                            level0name = "dbo",
                            level1type = "TABLE",
                            level1name = r.Table_Name,
                            level2type = "COLUMN",
                            level2name = r.COLUMN_NAME,
                            value = r.備註
                        }, commandType: System.Data.CommandType.StoredProcedure);
                    }
                    catch (SqlException e)
                    {
                        conn.Execute("sp_addextendedproperty", new
                        {
                            name = "REMARK",
                            level0type = "SCHEMA",
                            level0name = "dbo",
                            level1type = "TABLE",
                            level1name = r.Table_Name,
                            level2type = "COLUMN",
                            level2name = r.COLUMN_NAME,
                            value = r.備註
                        }, commandType: System.Data.CommandType.StoredProcedure);
                    }
                }
            }

            var emp = HttpContext.Session.GetObject<TEmployee>("Current_User");
            

            logger.LogInformation("123", "Log Information");
            XMLEditor x = new XMLEditor();
            var o = HttpContext.Session.GetObject<TResult>("Result_Old");
            x.Log(DateTime.Now.ToString(), emp.FAccount, r, o);

            return RedirectToAction("Index");
        }

        public IActionResult EditTable(string Id)
        {           
            if (Id != null && Id != "All")
            {
                TResult result;
                using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    DynamicParameters pm = new DynamicParameters();
                    pm.Add("@TName", Id, dbType: System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    result = conn.Query<TResult>("dbo.spTable", pm, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                }

                return PartialView("EditTable", result);
            }
            return RedirectToAction("Index");
        }

        public IActionResult LogList()
        {
            XMLEditor x = new XMLEditor();
            List<CLog> l = x.CreateLogList();
            

            return PartialView("LogList", l);
        }

    }
}
