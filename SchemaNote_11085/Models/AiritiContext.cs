using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SchemaNote_A11085.Models
{
    public class AiritiContext : Controller
    {
        //public bool CheckAccount(string cname)
        //{
            //try
            //{               
            //    string Constr = @"Data Source=JAY\SQLEXPRESS;Initial Catalog=AiritiCheck;Integrated Security=True";
               
            //    SqlConnection conn = new SqlConnection(Constr);

            //    string Sqlstr = "select cname from customer where cname = '" + cname + "'";
             
            //    SqlDataAdapter da = new SqlDataAdapter(Sqlstr, conn);

            //    DataSet ds = new DataSet();
                          
            //    da.Fill(ds);

        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        public IActionResult CheckAcc()
        {
            return View();
        }
    }
}

