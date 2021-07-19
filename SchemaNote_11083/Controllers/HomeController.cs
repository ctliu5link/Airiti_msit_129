using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchemaNote_11083.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using SchemaNote_11083.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;

namespace SchemaNote_11083.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration p_configuration)
    {
      _logger = logger;
      _configuration = p_configuration;
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult schemaNote()
    {
      return View();
    }

    /// <summary>
    /// 依照指定的連接字串與方法取得資料庫的Schema
    /// </summary>
    /// <param name="DBConnection">指定連接字串</param>
    /// <param name="Method">取得資料的方法1.ADO 2.Reflection 3.Airiti 4.Dapper </param>
    /// <returns></returns>
    public ActionResult DBdata(string DBConnection,string Method)
    {
      #region 變數宣告
      TtableSchema<IEnumerable<Ttable>> result = null;
      #endregion

      try
      {
        result = new DBSchema_Service().getDBSchema(DBConnection, Method); 
        HttpContext.Session.SetString(CDictionary.Current_DBConnection, DBConnection);
        ViewBag.ConnectionToken = CDictionary.Current_DBConnection;
        return result.IsNull ?
          Content("error") : PartialView("DBdata", result);
      }
      catch(Exception e)
      {
        return Content("error");
      }
    }

    /// <summary>
    /// 更新資料庫的擴充屬性
    /// </summary>
    /// <param name="data">欲更新的資料內容</param>
    [HttpPost]
    public void updateExpendedProperty([FromBody] tUpdateExpendedProperty data)
    {
      string dbconnection = HttpContext.Session.GetString(CDictionary.Current_DBConnection); //從Session取出目前連接資料庫的連接字串
      new DBSchema_Service().updateExpendedProperty(data, dbconnection); 
    }

    public ActionResult keepSession(string token)
    {
      if (string.IsNullOrEmpty(HttpContext.Session.GetString(token)))
        return Content("lost");
      return Content("ok");
    }




  }


}
