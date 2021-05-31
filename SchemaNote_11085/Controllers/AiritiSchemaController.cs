using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            ScheNoteViewModel SchemaNoteVM = new ScheNoteViewModel();
                //var table = from c in (new AiritiCheckContext()).Accounts
                //            select c;
            AiritiDB();
            List<CAccountViewModel> list = new List<CAccountViewModel>();
           
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                //AiritiTable A = new AiritiTable();
                CAccountViewModel model = new CAccountViewModel();
                model.Table_Name = item["欄位名稱"].ToString();
                model.Table_Description = item["欄位說明"].ToString();
                model.Table_PK = item["主鍵"].ToString();
                model.Table_Type = item["資料型態"].ToString();
                model.Table_IsNullable = item["不為Null"].ToString();
                model.Table_Default = item["預設值"].ToString();
                model.Table_Remark = item["備註"].ToString();

                //A.Table_Name = item["欄位名稱"].ToString();
                //A.Table_Description = item["欄位說明"].ToString();
                //A.Table_PK = item["主鍵"].ToString();
                //A.Table_Type = item["資料型態"].ToString();
                //A.Table_IsNullable = item["不為Null"].ToString();
                //A.Table_Default = item["預設值"].ToString();
                //A.Table_Remark = item["備註"].ToString();
                //list.Add(new CAccountViewModel(A));
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
                string comm = @"select  C.COLUMN_NAME AS '欄位名稱',sep.value as '欄位說明',k.type as'主鍵',
                (DATA_TYPE+'('+CONVERT(nvarchar,CHARACTER_MAXIMUM_LENGTH)+')')AS '資料型態',
                c.IS_NULLABLE As '不為Null',COLUMN_DEFAULT As '預設值',sep2.value as '備註'
                from AiritiCheck.INFORMATION_SCHEMA.Columns as c
                LEFT JOIN AiritiCheck.sys.types AS ty ON ty.name = c.DATA_TYPE
                LEFT JOIN AiritiCheck.INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.CONSTRAINT_CATALOG = c.TABLE_CATALOG 
                AND ISK.TABLE_NAME=c.TABLE_NAME 
                AND ISK.COLUMN_NAME=C.COLUMN_NAME
                LEFT JOIN AiritiCheck.sys.key_constraints AS k ON k.name=ISK.CONSTRAINT_NAME
                LEFT JOIN AiritiCheck.sys.tables AS tb ON tb.name=c.TABLE_NAME
                LEFT JOIN AiritiCheck.sys.extended_properties as sep ON sep.minor_id=c.ORDINAL_POSITION 
                And sep.major_id='581577110' 
                And sep.name ='MS_Description'
                LEFT JOIN AiritiCheck.sys.extended_properties AS sep2 ON sep.minor_id=sep2.minor_id 
                And sep.major_id=sep2.major_id 
                And sep2.name = 'REMARK'
                where c.TABLE_NAME in ('Account','Account_ETDS','Account_SchInfo','Account_SchInfoRefuse','AccountRefuse') ";
                SqlConnection Connection = new SqlConnection(conn);//使用連接字串初始SqlConnection物件連接資料庫
                SqlCommand command = new SqlCommand($"{comm}", Connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(ds);
            }
            catch
            {

            }
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
