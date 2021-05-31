using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_A11087.Models
{
    public class CAccountFactory
    {
        private void excusteSql(string sql,List<SqlParameter> paras)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source = DESKTOP - 13I52L2\SQLEXPRESS; Initial Catalog = AiritiCheck; Integrated Security = True";
            con.Open();
            
            SqlCommand cmd = new SqlCommand(sql,con);
            if (paras != null)
            {
                foreach (SqlParameter p in paras)
                    cmd.Parameters.Add(p);                  
            }
            cmd.ExecuteNonQuery();
            con.Close();

            //SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from Account",con);            
        }
    }
}
