using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelBinding_11084.Models;
using ModelBinding_11084.ViewModels;

namespace ModelBinding_11084.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            TemplateViewModel SimpleBinding = new TemplateViewModel("SimpleBinding", "(string Name, int Age)", new { Name = "Tom", Age = 15 }, "text");
            Human Tom = new Human() { Name = "Tom", Age = 15 };
            TemplateViewModel ModelBindObj = new TemplateViewModel("ModelBindObj", "(Human data)", Tom, "json");
            TemplateViewModel SimpleModelBindArray = new TemplateViewModel("SimpleModelBindArray", "(string[] Name, int[] Age)", new { Name = new string[] { "Tom", "Jack" }, Age = new int[] { 15, 16 } }, "text");

            return View(TemplateViewModel.lisTemplate);
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
                    //fileBase.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/Files/" + fileBase.FileName));
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
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet/*application/octet-stream*/, "img.png");
        }

    }
}