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
                    var tables = conn.Query<Ttable_dapper>(dapper_getAllTables());
                    foreach (var table in tables)
                    {
                        table.TtableColumns =
                            conn.Query<TtableColumn>(dapper_getTableColumns(), new { Table_Name = table.Table_Name });
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

        public string dapper_getTableColumns()
        {
            return @"select 
	sc.name as Column_Name, 
	case when ISNULL(ik.COLUMN_NAME,'') = '' then 0
	else 1
	end PK,
	case when ic.IS_NULLABLE ='YES' then 1
	else 0 
	end IS_Nullable,
	ic.DATA_TYPE + case
		when ISNULL(ic.CHARACTER_MAXIMUM_LENGTH,'')='' then ''
		else '(' + cast(ic.CHARACTER_MAXIMUM_LENGTH as varchar) + ')'
		end Data_Type,
	ISNULL(ic.COLUMN_DEFAULT,'') as Column_Default,
	ISNULL(epcd.value,'') as Description,
	ISNULL(epcr.value,'') as REMARK
from sys.tables st
inner join sys.columns sc
on st.object_id = sc.object_id
left join INFORMATION_SCHEMA.COLUMNS ic
on ic.TABLE_NAME = st.name
and ic.COLUMN_NAME = sc.name
left join sys.extended_properties epcd
on epcd.major_id = st.object_id
and epcd.minor_id = sc.column_id
and epcd.name ='MS_Description'
left join sys.extended_properties epcr
on epcr.major_id = st.object_id
and epcr.minor_id = sc.column_id
and epcr.name ='REMARK'
left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE ik
on st.name = ik.TABLE_NAME
and sc.name = ik.COLUMN_NAME
and left(ik.CONSTRAINT_NAME,2)='PK'
where st.name = @Table_Name
order by sc.column_id";
        }
        public string dapper_getAllTables()
        {
            return @"select 
	st.name as Table_Name, 
	ist.TABLE_SCHEMA as Table_Schema,
	st.create_date as Create_Date,
	st.modify_date as Modify_Date,
	sp.rows as Total_Rows,
	ISNULL(eptd.value,'') as Table_Description,
	ISNULL(eptr.value,'') as Table_REMARK
from sys.tables st
inner join INFORMATION_SCHEMA.TABLES ist
on st.name = ist.TABLE_NAME
inner join sys.partitions sp
on st.object_id = sp.object_id
and sp.index_id in (0,1)
left join sys.extended_properties eptd
on eptd.major_id = st.object_id
and eptd.minor_id = 0
and eptd.name ='MS_Description'
left join sys.extended_properties eptr
on eptr.major_id = st.object_id
and eptr.minor_id = 0
and eptr.name ='REMARK'
order by st.name";
        }
    }
}
