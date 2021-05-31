using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchemaNote_11084.Models;
using SchemaNote_11084.MyClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11084.Controllers
{
    public class FilterController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (HttpContext.Session.GetObject<TEmployee>(DDictionary.Current_User) == null) // session 不存在的話導向登入頁
                filterContext.Result = RedirectToAction("Login", "Login");
        }        
    }
}
