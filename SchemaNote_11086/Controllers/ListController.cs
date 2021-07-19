using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SchemaNote_11086.Models;
using SchemaNote_11086.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11086.Controllers
{
    public class ListController : Controller
    {
        // GET: ListController
        public IActionResult index() {


            return View();
        }
        [HttpPost]
        public IActionResult index(string db字串)
        {
            if (db字串 != null)
            {
                //處理字串 "";
                string db字串處理 = db字串.Replace(@"""", "");
                //只有TempData可以跨Action傳值
                //TempData 使用一次/逾時會消滅，使用TempData.Keep()延長生命週期，與TempData.Peek()有異
                TempData["db連結字串"] = db字串處理;
                //TempData.Keep("db連結字串");

                //Mvc Core 使用 Session需要註冊與啟用Startup.cs 設定
                //HttpContext.Session.SetString("DbConnection", db字串處理);
                Global.ConnectionString = db字串處理;
                return RedirectToAction("List");
            
            }
            return View("index");
        }
    

        public ActionResult List()
        { 
            SqlConnection conn = null;
            string dbconn = TempData["db連結字串"].ToString();
            //TempData.Keep("db連結字串");

            TableName[] Tables = null;
            try
            {
                using (conn = new SqlConnection(dbconn))
                {
                    //string conn = @"Data Source =.; Initial Catalog = AiritiCheck; Integrated Security = True";
                    string comm = sqlCommand();
                    //SqlConnection nowConnection = new SqlConnection(conn); //使用連接字串初始SqlConnection物件連接資料庫

                    SqlCommand command = new SqlCommand(comm, conn);
                    //comm.parameter;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    //傳入的是select字串:sqlDataAdapter.SelectCommand=new SqlCommand("SQL")
                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);

                    List<TableName> tablelist = new List<TableName>();
                    List<TableColumn> columnslist = new List<TableColumn>();

                    var dtTable = dataSet.Tables[1];//所有tablename

                    Tables = new TableName[dtTable.Rows.Count];
                    int tableCount = 0;
                    foreach (DataRow titeam in dtTable.Rows)
                    {
                        //TableName tableName = new TableName()
                        //{
                        //    Table_Name = titeam["Table_Name"].ToString()
                        //};
                        TableName tableName = new TableName();
                        tableName.Table_Name = titeam["Table_Name"].ToString();

                        //設定此TableName下的TableColumn數量，沒設定下段foreach,columnCount++，會不知道上限為多少
                        tableName.TtableColumns = new TableColumn[(int)titeam["欄位總數"]];

                        Tables[tableCount] = tableName;
                        tableCount++;
                        tablelist.Add(tableName);
                    }

                    TableName TableName抓取 = null;
                    int columnCount = 0;
                    foreach (DataRow citeam in dataSet.Tables[0].Rows)
                    {
                        if (TableName抓取 == null || citeam["Table_Name"].ToString() != TableName抓取.Table_Name)
                        {
                            columnCount = 0;
                            TableName抓取 = Tables.FirstOrDefault(n => n.Table_Name == citeam["Table_Name"].ToString());
                            TableName抓取.Table_Description = citeam["表說明"].ToString();
                            TableName抓取.Table_REMARK = citeam["表備註"].ToString();
                            TableName抓取.Table_Schema = citeam["Table_Schema"].ToString();
                            TableName抓取.Create_Date = citeam["創建時間"].ToString();
                            TableName抓取.Modify_Date = citeam["修改時間"].ToString();

                        }
                        TableColumn tableColumn = new TableColumn();
                        tableColumn.Column_Name = citeam["Column_Name"].ToString();
                        tableColumn.Data_Type= citeam["資料型別"].ToString();
                        tableColumn.IS_Nullable = citeam["允許空值"].ToString() == "YES" ? 1 : 0;
                        tableColumn.PK = citeam["主鍵"].ToString() == "PK" ? 1:0;
                        tableColumn.Column_Default= citeam["預設值"].ToString();
                        tableColumn.Description= citeam["欄位說明"].ToString();
                        tableColumn.REMARK = citeam["欄位備註"].ToString();

                        var a = TableName抓取.TtableColumns[columnCount] = tableColumn;
                        columnCount++;
                        columnslist.Add(a);
                    }
                    inputViewModel inputModel = new inputViewModel();
                    inputModel.tableNames = tablelist;
                    inputModel.tableColumns = columnslist;

                    #region 舊code
                    //List<SchemaNoteViewModel> list = new List<SchemaNoteViewModel>();
                    //List<SchemaNoteViewModel> list2 = new List<SchemaNoteViewModel>();

                    //foreach (DataRow item in dataSet.Tables[0].Rows)//選擇第幾張表
                    //{
                    //    SchemaNote schemaNote = new SchemaNote();
                    //    schemaNote.tableName = item["Table_Name"].ToString();
                    //    schemaNote.t創建時間 = item["創建時間"].ToString();
                    //    schemaNote.t修改時間 = item["修改時間"].ToString();
                    //    schemaNote.t結構描述 = item["Table_Schema"].ToString();
                    //    schemaNote.t表備註 = item["表備註"].ToString();
                    //    schemaNote.t表說明 = item["表說明"].ToString();

                    //    schemaNote.columnName = item["Column_Name"].ToString();
                    //    schemaNote.c不為Null = item["允許空值"].ToString();
                    //    schemaNote.c主鍵 = item["主鍵"].ToString();
                    //    schemaNote.c欄位備註 = item["欄位備註"].ToString();
                    //    schemaNote.c欄位說明 = item["欄位說明"].ToString();
                    //    schemaNote.c資料型別 = item["資料型別"].ToString();
                    //    schemaNote.c預設值 = item["預設值"].ToString();

                    //    list.Add(new SchemaNoteViewModel(schemaNote));
                    //}
                    //foreach (DataRow item in dataSet.Tables[1].Rows)
                    //{
                    //    SchemaNote schemaNote = new SchemaNote();
                    //    schemaNote.tableName = item["Table_Name"].ToString();
                    //    schemaNote.t結構描述 = item["Table_Schema"].ToString();
                    //    schemaNote.t創建時間 = item["創建時間"].ToString();
                    //    schemaNote.t修改時間 = item["修改時間"].ToString();
                    //    schemaNote.t表備註 = item["表備註"].ToString();
                    //    schemaNote.t表說明 = item["表說明"].ToString();


                    //    list2.Add(new SchemaNoteViewModel(schemaNote));
                    //}

                    //inputModel inputModel = new inputModel();

                    //inputModel.show = list;
                    //inputModel.showTableNAme = list2;
                    #endregion 

                    return View(inputModel);
                    //return View(list);  //view 需不同modle 接收
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
        }

        [HttpPost]
        public void updataSchema([FromBody] UpdateExpendedProperty data)
        {

            //強制 Web API 從要求主體讀取簡單類型
            //一个函数中， 最多只能有一个[FromBody] 标记， 因为客户端的请求有可能没有缓冲， 只能被读取一次。
            //当参数具有[FromBody]时，Web API将使用Content-Type标头来选择格式器。在此示例中，内容类型为“ application / json”，请求主体为原始JSON字符串（不是JSON对象）
            //通過該標識來指定該參數值需從請求的正文中獲取而不是從URL中獲取。由於URL的長度限制所以無法攜帶較長的數據信息，當攜帶的數據超過瀏覽器的最大URL長度時請求將被瀏覽器拒絕，這時我們就需要通過請求體來發送數據了。

            //string dbconn = @"Data Source =.; Initial Catalog = AiritiCheck; Integrated Security = True";
            string dbconn = Global.ConnectionString; ;
            //string dbconn = TempData["db連結字串"].ToString();
            //TempData.Keep("db連結字串");
            //string dbconn = HttpContext.Session.GetString("DbConnection");

            using (SqlConnection conn = new SqlConnection(dbconn))
            {
                try
                {
                    conn.Open();
                    data.target = data.target == "Description" ? "MS_Description" : data.target;
                    string strcommand = data.column == "" ?
                        $"EXEC sys.sp_updateextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table" :
                        $"EXEC sys.sp_updateextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table, @level2type=N'COLUMN',@level2name=@data_column";
                    SqlCommand command = new SqlCommand(strcommand, conn);
                    //使用用 Parameter 將會提高安全性，若使用者輸入了特別符號，也比較不會出問題

                    command.Parameters.Add(new SqlParameter("@data_target", data.target));
                    command.Parameters.Add(new SqlParameter("@data_value", data.value));
                    command.Parameters.Add(new SqlParameter("@data_table", data.table));
                    command.Parameters.Add(new SqlParameter("@data_column", data.column));

                    //ExecuteNonQuery用來執行INSERT、UPDATE、DELETE和其他沒有返回值的SQL命令。例如：CREATE DATABASE 和 CREATE TABLE 命令。
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



        public string sqlCommand()
        {
            return
                 @"select 
	st.name as Table_Name, 
	ic.TABLE_SCHEMA as Table_Schema,
	CONVERT(varchar(10),st.create_date,23) as 創建時間,
	CONVERT(varchar(10),st.modify_date,23) as 修改時間,
	sp.rows as Total_Rows,
	sc.name as Column_Name,
	case when ISNULL(ik.COLUMN_NAME,'') = '' then ''
	else 'PK'
	end 主鍵,
    ic.IS_NULLABLE as 允許空值,
	ic.DATA_TYPE + case
		when ISNULL(ic.CHARACTER_MAXIMUM_LENGTH,'')='' then ''
		else '(' + cast(ic.CHARACTER_MAXIMUM_LENGTH as varchar) + ')'
		end 資料型別,
	ISNULL(ic.COLUMN_DEFAULT,'') as 預設值,
	ISNULL(cde.value,'') as 欄位說明,
	ISNULL(cre.value,'') as 欄位備註,
	ISNULL(tde.value,'') as 表說明,
	ISNULL(tre.value,'') as 表備註
from sys.tables st
inner join sys.columns sc
on st.object_id = sc.object_id
inner join sys.partitions sp
on st.object_id = sp.object_id
and sp.index_id in (0,1)
left join INFORMATION_SCHEMA.COLUMNS ic
on ic.TABLE_NAME = st.name
and ic.COLUMN_NAME = sc.name
left join sys.extended_properties cde
on cde.major_id = st.object_id
and cde.minor_id = sc.column_id
and cde.name ='MS_Description'
left join sys.extended_properties cre
on cre.major_id = st.object_id
and cre.minor_id = sc.column_id
and cre.name ='REMARK'
left join sys.extended_properties tde
on tde.major_id = st.object_id
and tde.minor_id = 0
and tde.name ='MS_Description'
left join sys.extended_properties tre
on tre.major_id = st.object_id
and tre.minor_id = 0
and tre.name ='REMARK'
left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE ik
on st.name = ik.TABLE_NAME
and sc.name = ik.COLUMN_NAME
and left(ik.CONSTRAINT_NAME,2)='PK'
order by st.name,sc.column_id " +  //不同查詢方法記得空格

 @"select
t.name as Table_Name,
t.create_date as 創建時間,
t.modify_date as 修改時間,
ic.TABLE_SCHEMA as Table_Schema,
	ISNULL(tde.value,'') as 表說明,
	ISNULL(tre.value,'') as 表備註,
count(t.name) as 欄位總數
from sys.tables t
inner join sys.columns sc
on t.object_id = sc.object_id
left join INFORMATION_SCHEMA.COLUMNS ic
on ic.TABLE_NAME = t.name
and ic.COLUMN_NAME = sc.name
left join sys.extended_properties tde
on tde.major_id = t.object_id
and tde.minor_id = 0
and tde.name ='MS_Description'
left join sys.extended_properties tre
on tre.major_id = t.object_id
and tre.minor_id = 0
and tre.name ='REMARK'
group by t.name,t.create_date,t.modify_date,ic.TABLE_SCHEMA,
	ISNULL(tde.value,''),
	ISNULL(tre.value,'')";
        }
    }
}

