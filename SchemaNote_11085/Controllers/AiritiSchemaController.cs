using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchemaNote_11085.Models;
using SchemaNote_A11085.Models;
using SchemaNote_A11085.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_A11085.Controllers
{
    public class AiritiSchemaController : Controller
    {       
        DataSet ds = new DataSet();
        AiritiCheckContext db = new AiritiCheckContext();
        //private readonly AiritiCheckContext db_AiritiCheck;
        //public AiritiSchemaController(AiritiCheckContext context)
        //{
        //    db_AiritiCheck = context;
        //}

        //public IEnumerable<CAccountViewModel> Get()
        //{
        //    var table = from c in (new AiritiCheckContext()).Accounts
        //                select c;

        //    List<CAccountViewModel> list = new List<CAccountViewModel>();
        //    foreach (Account n in table)
        //    {

        //        list.Add(new CAccountViewModel(n));
        //    }
        //    return list;
        //}      

        public ActionResult List()
        {
            //ScheNoteViewModel SchemaNoteVM = new ScheNoteViewModel();
            //var table = from c in (new AiritiCheckContext()).Accounts
            //            select c;
            //string a = $"http\n....";
            //Console.WriteLine(a);
            //AiritiDB1();


            AiritiDB();

            List<Combine> list = new List<Combine>();
            IEnumerable<IGrouping<string, Combine>> q = null;
            foreach (DataRow item in ds.Tables[0].Rows)//選擇第幾張表
            {
                //TableColumn tc = new TableColumn();

                //tc.Main_UserTable = item["表欄位說明"].ToString();          
                //tc.DescriptionName = item["DescriptionName"].ToString();
                //tc.Object_CreateDay = item["Object_CreateDay"].ToString();
                //tc.Object_UpdateDay = item["Object_UpdateDay"].ToString();
                //tc.TotalCount = item["TotalCount"].ToString();
                //tc.Remark = item["表備註"].ToString();

                //AiritiTable A = new AiritiTable();
                Combine model = new Combine();
                //model.b = new Models.b();
                //model.a = new Models.a();
                //ColumnViewModel model = new ColumnViewModel();
                model.b.TableName = item["TableName"].ToString();
                model.b.Main_UserTable = item["表欄位說明"].ToString();
                model.b.DescriptionName = item["DescriptionName"].ToString();
                model.b.Object_CreateDay = item["Object_CreateDay"].ToString();
                model.b.Object_UpdateDay = item["Object_UpdateDay"].ToString();
                model.b.TotalCount = item["TotalCount"].ToString();
                model.b.Remark = item["表備註"].ToString();
                model.a.Column_Name = item["欄位名稱"].ToString();
                model.a.Column_Description = item["欄位說明"].ToString();
                //if (item["主鍵"].ToString() == "PK")
                //{
                //    model.a.Column_PK = 1;
                //}
                //else
                //{
                //    model.a.Column_PK = 0;
                //}
                model.a.Column_IsNullable = (item["不為Null"].ToString() == "Yes") ? 1 : 0;
                model.a.Column_Type = item["資料型態"].ToString();
                //model.a.Column_IsNullable = item["不為Null"].ToString();
                if (item["不為Null"].ToString() == "Yes")
                {
                    model.a.Column_IsNullable = 1;
                }
                else
                {
                    model.a.Column_IsNullable = 0;
                }          
               model.a.Column_Default = item["預設值"].ToString();
                model.a.Column_Remark = item["備註"].ToString();
                list.Add(model);
               
                q = from L in list
                        group L by L.b.TableName into X
                        select X;
                                
            }
            //ScheNoteViewModel schema = new ScheNoteViewModel();
            //schema.SchemaNote = list;            
            return View(q);         
        }

        public void AiritiDB()
        {
            try
            {
                //string dbStringconn = TempData["Entry"].ToString();
                string conn =
                //dbStringconn;
                @"Data Source =.; Initial Catalog = AiritiCheck; Integrated Security = True";
                //@"Data Source=JAY\SQLEXPRESS;Initial Catalog=AiritiCheck;Integrated Security=True";
                string comm = @"select DISTINCT c.TABLE_NAME as TableName ,C.COLUMN_NAME AS '欄位名稱',sep.value as '欄位說明',k.type as'主鍵',
                (DATA_TYPE+'('+CONVERT(nvarchar,CHARACTER_MAXIMUM_LENGTH)+')')AS '資料型態',
                c.IS_NULLABLE As '不為Null',COLUMN_DEFAULT As '預設值',sep2.value as '備註',
				tb.create_date as Object_CreateDay,tb.modify_date as Object_UpdateDay,c.TABLE_SCHEMA as DescriptionName,sp.rows as TotalCount,
				ISNULL(tsep.value,'') as '表欄位說明',ISNULL(tsep1.value,'') as '表備註'
                from AiritiCheck.INFORMATION_SCHEMA.Columns as c
                LEFT JOIN AiritiCheck.sys.types AS ty ON ty.name = c.DATA_TYPE
                LEFT JOIN AiritiCheck.INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.CONSTRAINT_CATALOG = c.TABLE_CATALOG 
                AND ISK.TABLE_NAME=c.TABLE_NAME 
                AND ISK.COLUMN_NAME=C.COLUMN_NAME
                LEFT JOIN AiritiCheck.sys.key_constraints AS k ON k.name=ISK.CONSTRAINT_NAME
                LEFT JOIN AiritiCheck.sys.tables AS tb ON tb.name=c.TABLE_NAME                      
				inner join sys.columns sc
                on tb.object_id = sc.object_id
                inner join sys.partitions sp
                on tb.object_id = sp.object_id
				left join sys.extended_properties tsep
				on tsep.major_id = tb.object_id
				and tsep.minor_id = 0
				and tsep.name ='MS_Description'
				left join sys.extended_properties tsep1
				on tsep1.major_id = tb.object_id
				and tsep1.minor_id = 0
				and tsep1.name ='REMARK'     
				LEFT JOIN AiritiCheck.sys.extended_properties as sep ON sep.minor_id=c.ORDINAL_POSITION
				And sep.major_id='581577110' 
				And sep.name ='MS_Description'
				LEFT JOIN AiritiCheck.sys.extended_properties AS sep2 ON sep.minor_id=sep2.minor_id 
				And sep.major_id=sep2.major_id 
				And sep2.name = 'REMARK'
                where c.TABLE_NAME in ('Account','Account_ETDS','Account_SchInfo','Account_SchInfoRefuse','AccountRefuse')";
                    
                SqlConnection Connection = new SqlConnection(conn);//使用連接字串初始SqlConnection物件連接資料庫
                SqlCommand command = new SqlCommand($"{comm}", Connection);          
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);               
                sqlDataAdapter.Fill(ds);
            }
            catch
            {

            }
        }

        //public IActionResult ConnectionString()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult ConnectionString(string DbConn)
        //{
        //    if (DbConn != null)
        //    {
        //        string DbConnString = DbConn.Replace(@"""", "");
        //        TempData["Entry"] = DbConnString;
        //        return RedirectToAction("List");
        //    }
        //    return View("ConnectionString");
        //}

        /// <summary>
        /// 舊版 暫時註解掉
        /// </summary>
        /// <returns></returns>
        //public ViewResult AiritiDB()
        //{             
        //    try
        //    {            
        //        string conn = @"Data Source=JAY\SQLEXPRESS;Initial Catalog=AiritiCheck;Integrated Security=True";
        //        string comm = @"select  C.COLUMN_NAME AS '欄位名稱',sep.value as '欄位說明',k.type as'主鍵',
        //        (DATA_TYPE+'('+CONVERT(nvarchar,CHARACTER_MAXIMUM_LENGTH)+')')AS '類型',
        //        c.IS_NULLABLE As '不為Null',COLUMN_DEFAULT As '預設值',sep2.value as '備註'
        //        from AiritiCheck.INFORMATION_SCHEMA.Columns as c
        //        LEFT JOIN AiritiCheck.sys.types AS ty ON ty.name = c.DATA_TYPE
        //        LEFT JOIN AiritiCheck.INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.CONSTRAINT_CATALOG = c.TABLE_CATALOG 
        //        AND ISK.TABLE_NAME=c.TABLE_NAME 
        //        AND ISK.COLUMN_NAME=C.COLUMN_NAME
        //        LEFT JOIN AiritiCheck.sys.key_constraints AS k ON k.name=ISK.CONSTRAINT_NAME
        //        LEFT JOIN AiritiCheck.sys.tables AS tb ON tb.name=c.TABLE_NAME
        //        LEFT JOIN AiritiCheck.sys.extended_properties as sep ON sep.minor_id=c.ORDINAL_POSITION 
        //        And sep.major_id='581577110' 
        //        And sep.name ='MS_Description'
        //        LEFT JOIN AiritiCheck.sys.extended_properties AS sep2 ON sep.minor_id=sep2.minor_id 
        //        And sep.major_id=sep2.major_id 
        //        And sep2.name = 'REMARK'
        //        where c.TABLE_NAME ='Account'";
        //        SqlConnection Connection = new SqlConnection(conn);//使用連接字串初始SqlConnection物件連接資料庫
        //        SqlCommand command = new SqlCommand($"{comm}", Connection);
        //        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
        //        DataSet ds = new DataSet();
        //        sqlDataAdapter.Fill(ds);

        //        List<CAccountViewModel> list = new List<CAccountViewModel>();

        //        //foreach (DataRow t in ds.Tables[0].Rows)
        //        //{
        //        //    AiritiTable A = new AiritiTable();
        //        //    A.Table_Name = t["欄位名稱"].ToString();
        //        //    list.Add(new CAccountViewModel(A));
        //        //}
        //        foreach (DataRow t in ds.Tables[0].Rows)
        //        {
        //            AiritiTable A = new AiritiTable();
        //            A.Table_Name = t["欄位名稱"].ToString();
        //            list.Add(new CAccountViewModel(A));
        //        }
        //        ScheNoteViewModel schema = new ScheNoteViewModel();
        //        schema.SchemaNote = list;

        //        return View(schema);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //    return View();
        //}
    }
}
