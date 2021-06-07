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

        public IActionResult List()
        {
            //ScheNoteViewModel SchemaNoteVM = new ScheNoteViewModel();
            //var table = from c in (new AiritiCheckContext()).Accounts
            //            select c;
            //string a = $"http\n....";
            //Console.WriteLine(a);
            AiritiDB1();
            AiritiDB();

            List<ColumnViewModel> list = new List<ColumnViewModel>();
                      
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
                ColumnViewModel model = new ColumnViewModel();

                model.TableName = item["TableName"].ToString();
                model.Main_UserTable = item["表欄位說明"].ToString();
                model.DescriptionName = item["DescriptionName"].ToString();
                model.Object_CreateDay = item["Object_CreateDay"].ToString();
                model.Object_UpdateDay = item["Object_UpdateDay"].ToString();
                model.TotalCount = item["TotalCount"].ToString();
                model.Remark = item["表備註"].ToString();
                model.Column_Name = item["欄位名稱"].ToString();
                model.Column_Description = item["欄位說明"].ToString();        
                if (item["主鍵"].ToString() == "PK")
                {
                    model.Column_PK = 1;
                }
                else
                {
                    model.Column_PK = 0;
                }           
                model.Column_Type = item["資料型態"].ToString();
                if (item["不為Null"].ToString() == "Yes")
                {
                    model.Column_IsNullable = 1;
                }
                else
                {
                    model.Column_IsNullable = 0;
                }
                model.Column_Default = item["預設值"].ToString();
                model.Column_Remark = item["備註"].ToString();

             
                list.Add(model);
            }
            //ScheNoteViewModel schema = new ScheNoteViewModel();
            //schema.SchemaNote = list;            
            return View(list);         
        }

        public void AiritiDB()
        {
            try
            {
                string conn = @"Data Source=JAY\SQLEXPRESS;Initial Catalog=AiritiCheck;Integrated Security=True";
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

        public void AiritiDB1()
        {
            try
            {
                string conn1 = @"Data Source=JAY\SQLEXPRESS;Initial Catalog=AiritiCheck;Integrated Security=True";             

                string comm1 = @"select st.name as Table_Name,count(st.name) as Column_qty
                                                from sys.tables st inner join INFORMATION_SCHEMA.COLUMNS ic on st.name = ic.TABLE_NAME group by st.name";
                SqlConnection Connection1 = new SqlConnection(conn1);
                SqlCommand command1 = new SqlCommand($"{comm1}", Connection1);
                SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(command1);          
                sqlDataAdapter1.Fill(ds);
            }
            catch
            {

            }
        }

        public IActionResult ConnectionString ()
        {
            return View();
        }
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
