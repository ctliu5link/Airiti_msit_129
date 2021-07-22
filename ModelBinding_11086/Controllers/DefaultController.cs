using ModelBinding_11086.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelBinding_11086.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult ModelBinding()
        {
            return View();
        }
        public ActionResult SimpleBinding(string Name, int Age)
        {
            return Content(nameof(Name) + ":" + Name + ", " + nameof(Age) + ":" + Age);
        }

        public ActionResult ModelBindObj(Human data)
        {
            return Json(data);
        }

        public ActionResult SimpleModelBindArray(string[] Name, int[] Age)
        {
            string str = nameof(Name) + ":";
            foreach (string n in Name)
            {
                str += n + ",";
            }
            //從這個執行個體擷取子字串。 子字串會在指定的字元位置開始並繼續到字串的結尾。
            str = str.Substring(0, str.Length - 1);
            str += " | ";
            str += nameof(Age) + ":";
            foreach (int a in Age)
            {
                str += a + ",";
            }
            str = str.Substring(0, str.Length - 1);
            return Content(str);
        }

        public ActionResult ModelBindingArray(Human[] data)
        {
     
            var a = data;
            return Json(data);
        }

        public ActionResult ModelBindingNestedObj(Person data)
        {
            return Json(data);
        }

        public ActionResult ModelBindingArrayNestedObj(Person[] data)
        {
            return Json(data);
        }

        public ActionResult SendFile(HttpPostedFileBase fileBase)
        {
            byte[] bytes = new byte[0];
            string fileName = "";
            if (fileBase != null)
            {
                if (fileBase.ContentLength > 0)
                {
                    bytes = new byte[fileBase.ContentLength];
                    fileBase.InputStream.Read(bytes, 0, fileBase.ContentLength);
                    fileBase.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/Files/" + fileBase.FileName));
                    fileBase.InputStream.Flush();
                }
                fileName = fileBase.FileName;
            }
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet/*application/octet-stream*/, fileName);
        }

        public ActionResult SendByte()
        {
            var r = Request;
            byte[] bytes = r.BinaryRead(r.ContentLength);
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet/*application/octet-stream*/, "照片.png");
        }

    }
}