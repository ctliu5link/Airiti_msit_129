using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SchemaNote_11087.Models;
using SchemaNote_A11087.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_A11087.Controllers
{
    public class AccountController : Controller
    {
        public static string _ConnectionString { get; set; }

        public AccountController(IOptions<ConnectionStringConfig> config)
        {
            _ConnectionString = config.Value.DBConStr;
        }
        public IActionResult Index(string tableName = "Account")
        {
            var tableNames = GetTableNames();

            ViewBag.TableNames = tableNames;

            List<TableModel> result = GetTableDesc(tableName);

            return View(result);
        }
        
        [HttpPost]
        public void Edit([FromBody] TablePropertyEditModel tableProperty)
        {
            string sql = $"EXEC sys.sp_updateextendedproperty @name=@Item, @value=@Value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@TableName";
            if (string.IsNullOrWhiteSpace(tableProperty.ColumnName) == false)
            {
                sql = $"{sql}, @level2type=N'COLUMN',@level2name=@ColumnName";
            }
            ExecuteSql(tableProperty, sql);
        }
        private List<string> GetTableNames()
        {
            string sql = @"
SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
";
            return QueryList<string>(sql);
        }
        private static List<TableModel> GetTableDesc(string tableName)
        {
            string sql = @"
SELECT
    b.COLUMN_NAME ColumnName
	,b.TABLE_SCHEMA TableSchema
    ,b.DATA_TYPE DataTpye
    ,b.CHARACTER_MAXIMUM_LENGTH CharacterMaximumLength
    ,b.COLUMN_DEFAULT ColumnDefault
    ,b.IS_NULLABLE IsNullable
	,FORMAT(d.create_date, 'd', 'zh-cn' ) CreateDate
	, FORMAT(d.modify_date, 'd', 'zh-cn' ) ModifyDate
	,R.value Remark
	,M.value MSDescription
	,CASE When pk.COLUMN_NAME = b.COLUMN_NAME THEN 1 ELSE 0 end IsPK
FROM
    INFORMATION_SCHEMA.TABLES  a
    LEFT JOIN INFORMATION_SCHEMA.COLUMNS b ON (a.TABLE_NAME=b.TABLE_NAME)
	LEFT JOIN sys.tables as d on d.name=b.TABLE_NAME COLLATE SQL_Latin1_General_CP1_CI_AS
	LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE as pk on pk.COLUMN_NAME=b.COLUMN_NAME
	LEFT JOIN fn_listextendedproperty(NULL, 'user', 'dbo', 'table', @TName, 'column',default) AS m on m.name='MS_Description' and b.COLUMN_NAME = m.objname COLLATE SQL_Latin1_General_CP1_CI_AS 
	LEFT JOIN fn_listextendedproperty(NULL, 'user', 'dbo', 'table', @TName, 'column',default) AS r on r.name='REMARK'and b.COLUMN_NAME = r.objname COLLATE SQL_Latin1_General_CP1_CI_AS 
WHERE
    TABLE_TYPE='BASE TABLE' and a.TABLE_NAME= @TName
ORDER BY
    a.TABLE_NAME, b.ordinal_position
";
            var result = QueryList<TableModel>(sql, new { TName = tableName });
            return result;
        }
        private static List<T> QueryList<T>(string sql, object param = null)
        {
            using (var conn = new SqlConnection(_ConnectionString))
            {
                var tables = conn.Query<T>(sql, param);
                return tables.ToList();
            }
        }
        private static void ExecuteSql(TablePropertyEditModel tableProperty, string sql)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                conn.Execute(sql, tableProperty);
            }
        }
    }
}
