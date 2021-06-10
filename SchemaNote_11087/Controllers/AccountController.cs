using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;//IOptions
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
        public string _ConnectionString { get; set; }//宣告全域變數

        public AccountController(IOptions<ConnectionStringConfig> config)
        {
            _ConnectionString = config.Value.DBConStr;
        }

        public IActionResult Index(string tableName = "Account")
        {
            var tableNames = GetTableNames();

            ViewBag.TableNames = tableNames;

            List<TableModel> result = GetTableInfo(tableName);

            return View(result);
        }

        [HttpPost]
        public void updateExpendedProperty([FromBody] TablePropertyEditModel tableProperty) //修改的4個參數>> 擴充名稱，擴充值，資料表名，跟欄位
        {
            string sql = $"EXEC sys.sp_updateextendedproperty @name=@Item, @value=@Value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@TableName";
            
            if (string.IsNullOrWhiteSpace(tableProperty.ColumnName) == false)//如果column的值'不'是null或是空值的話，就執行if內帶入第2層級
            {
                sql = $"{sql}, @level2type=N'COLUMN',@level2name=@ColumnName";
            }
            SqlCommand command = new SqlCommand();
            command.Parameters.Add(new SqlParameter("Item", tableProperty.Item));
            command.Parameters.Add(new SqlParameter("Value", tableProperty.Value));
            command.Parameters.Add(new SqlParameter("ColumnName", tableProperty.ColumnName));
            command.Parameters.Add(new SqlParameter("TableName", tableProperty.TableName));
            
            ExecuteSql(sql, command);
        }

        private List<string> GetTableNames()
        {
            string sql = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
            return QueryList<string>(sql);
        }
        private List<TableModel> GetTableInfo(string tableName)
        {
            string sql = @"
SELECT
	 b.TABLE_NAME TableName
    ,b.COLUMN_NAME ColumnName
	,b.TABLE_SCHEMA TableSchema
    ,b.DATA_TYPE DataType
    ,b.CHARACTER_MAXIMUM_LENGTH CharacterMaximumLength
    ,b.COLUMN_DEFAULT ColumnDefault
    ,b.IS_NULLABLE IsNullable
	,CASE When pk.COLUMN_NAME = b.COLUMN_NAME Then 1 else 0 end IsPK
	,sp.rows TotalRows
	,FORMAT(d.create_date, 'd', 'zh-cn' )  CreateDate
	,FORMAT(d.modify_date, 'd', 'zh-cn' ) ModifyDate
	,M.value MSDescription
	,R.value Remark
	,tm.value TableMSDescription
	,tr.value TableRemark
FROM
    sys.TABLES a
    LEFT JOIN INFORMATION_SCHEMA.COLUMNS b on (a.name=b.TABLE_NAME)
	left join sys.partitions as sp on a.object_id = sp.object_id
	LEFT JOIN sys.tables as d on d.name=b.TABLE_NAME COLLATE SQL_Latin1_General_CP1_CI_AS
	LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE as pk on pk.COLUMN_NAME=b.COLUMN_NAME
	LEFT JOIN fn_listextendedproperty(NULL, 'user', 'dbo', 'table', @TName, 'column',default) as m on m.name='MS_Description' and b.COLUMN_NAME = m.objname COLLATE SQL_Latin1_General_CP1_CI_AS 
	LEFT JOIN fn_listextendedproperty(NULL, 'user', 'dbo', 'table', @TName, 'column',default) as r on r.name='REMARK'and b.COLUMN_NAME = r.objname COLLATE SQL_Latin1_General_CP1_CI_AS 
	LEFT JOIN sys.extended_properties tm on tm.major_id=a.object_id and tm.minor_id = 0 and tm.name ='MS_Description'
	LEFT JOIN sys.extended_properties tr on tr.major_id=a.object_id and tr.minor_id = 0 and tr.name ='REMARK'
WHERE
    a.name= @TName";
            List<TableModel> result = QueryList<TableModel>(sql, new { TName = tableName });

            return result;
        }
        private List<T> QueryList<T>(string sql, object param = null)
        {
            return Query<T>(sql, param).ToList();
        }

        private T QueryFirst<T>(string sql, object param = null)
        {
            return Query<T>(sql, param).FirstOrDefault();
        }

        private IEnumerable<T> Query<T>(string sql, object param = null)//由此查詢
        {
            using (var conn = new SqlConnection(_ConnectionString))
            {
                var tables = conn.Query<T>(sql, param);
                return tables;
            }
        }

        private void ExecuteSql(string sql, object param = null)//to update insert delete
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {                
                conn.Execute(sql, param);
            }
        }
    }
}
