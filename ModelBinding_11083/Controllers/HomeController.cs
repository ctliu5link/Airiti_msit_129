using ModelBinding_11083.Models;
using ModelBinding_11083.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ModelBinding_11083.Controllers
{
  public class HomeController : Controller
  {

    public ActionResult Index()
    {
      return View(ModelBinding.getList());
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}