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
                TempData["db連結字串"] = db字串處理;
                return RedirectToAction("List");
            }
            return View("index");
        }
 
        public ActionResult List()
        {
            string dbconn = TempData["db連結字串"].ToString();

            DataSet ds = new DataSet();

            string conn = dbconn;
            //string conn = @"Data Source =.; Initial Catalog = AiritiCheck; Integrated Security = True";

            string comm = @"select t.name  tableName,c.name  columsName,ic.TABLE_SCHEMA as 結構描述,t.create_date as 創建時間,t.modify_date as 修改時間, pk.type as 主鍵,c.is_nullable 允許空值1,ic.IS_NULLABLE 允許空值2,ic.COLUMN_DEFAULT as 預設值, ic.DATA_TYPE  + CASE WHEN ic.CHARACTER_MAXIMUM_LENGTH IS NULL THEN '' ELSE '(' +  CAST(ic.CHARACTER_MAXIMUM_LENGTH AS varchar(3)) +')' END
    as 資料型別,sep.value 欄位說明,sep2.value 欄位備註,sep3.value 表說明,sep4.value 表備註

from sys.tables t
inner join sys.columns c on t.object_id = c.object_id
inner join INFORMATION_SCHEMA.Columns ic on c.name = ic.COLUMN_NAME
left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE as isk on isk.CONSTRAINT_CATALOG=ic.TABLE_CATALOG
AND ISK.TABLE_NAME=ic.TABLE_NAME
AND ISK.COLUMN_NAME=ic.COLUMN_NAME
left join sys.key_constraints pk on pk.name=isk.CONSTRAINT_NAME
left join sys.extended_properties sep on t.object_id = sep.major_id
and c.column_id = sep.minor_id
and sep.name = 'MS_Description'
left join sys.extended_properties sep2 on t.object_id = sep2.major_id
and c.column_id = sep2.minor_id
and sep2.name = 'REMARK'
left join sys.extended_properties sep3 on sep3.major_id = t.object_id
and sep3.minor_id = 0
and sep3.name = 'MS_Description'
left join sys.extended_properties sep4 on sep4.major_id = t.object_id
and sep4.minor_id = 0
and sep4.name = 'REMARK'


order by tableName";
            SqlConnection nowConnection = new SqlConnection(conn);//使用連接字串初始SqlConnection物件連接資料庫
            SqlCommand command = new SqlCommand($"{comm}", nowConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            //傳入的是select字串:sqlDataAdapter.SelectCommand=new SqlCommand("SQL")

            sqlDataAdapter.Fill(ds);

            List<SchemaNoteViewModel> list = new List<SchemaNoteViewModel>();

            foreach (DataRow t in ds.Tables[0].Rows)//選擇第幾張表
            {
                SchemaNote schemaNote = new SchemaNote();
                schemaNote.tableName = t["tableName"].ToString();
                schemaNote.t創建時間 = t["創建時間"].ToString();
                schemaNote.t修改時間 = t["修改時間"].ToString();
                schemaNote.t結構描述 = t["結構描述"].ToString();
                schemaNote.t表備註 = t["表備註"].ToString();
                schemaNote.t表說明 = t["表說明"].ToString();

                schemaNote.columnName = t["columsName"].ToString();
                schemaNote.c不為Null = t["允許空值2"].ToString();
                schemaNote.c主鍵 = t["主鍵"].ToString();
                schemaNote.c欄位備註 = t["欄位備註"].ToString();
                schemaNote.c欄位說明 = t["欄位說明"].ToString();
                schemaNote.c資料型別 = t["資料型別"].ToString();
                schemaNote.c預設值 = t["預設值"].ToString();

                list.Add(new SchemaNoteViewModel(schemaNote));
            }

            inputModel inputModel = new inputModel();

            inputModel.show = list;

            //return View(inputModel);
            return View(list);

        }

        // GET: ListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ListController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ListController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
