using Microsoft.AspNetCore.Mvc;
using SchemaNote_11084.Models;
using SchemaNote_11084.MyClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace SchemaNote_11084.Controllers
{    
    public class LoginController : Controller
    {
        static List<TEmployee> empList;

        string xmlPath = Directory.GetCurrentDirectory() + @"\config.xml";
        public IActionResult Login()
        {
            if (HttpContext.Session.GetObject<TEmployee>(DDictionary.Current_User) != null) //session 存在就跳回首頁
                return RedirectToAction("Index", "Home");                
                        
            XMLEditor xml = new XMLEditor();
            xml.CreateXML();
            empList = xml.CreateEmpList();

            return PartialView();
        }

        [HttpPost]
        public IActionResult Login(TEmployee e)
        {

            TEmployee empQuery = empList.FirstOrDefault(x => x.FAccount.Equals(e.FAccount) && x.FPassword.Equals(e.FPassword));
            if (empQuery != null)
            {
                HttpContext.Session.SetObject<TEmployee>(DDictionary.Current_User, empQuery);
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("Index", "SQL_Server");
            }
            else
                return PartialView();
        }

        [HttpPost]
        public JsonResult CheckLogin([FromBody] TEmployee p)
        {            
            var emp = empList.Where(a => a.FAccount == p.FAccount).FirstOrDefault();
            string Data = (emp == null) ? "無此帳號" : (emp.FPassword != p.FPassword) ? "密碼錯誤" : $"登入成功\nHi {p.FAccount}，歡迎使用本系統。";            

            return Json(Data);
        }
    }
}
