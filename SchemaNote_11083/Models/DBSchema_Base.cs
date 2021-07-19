using Microsoft.AspNetCore.Http;
using SchemaNote_11083.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Models
{

  public abstract class DBSchema_Base : IDBSchema
  {
    public abstract IEnumerable<Ttable> getSchema(string DBConnection);

    public virtual void updateExpendedProperty(tUpdateExpendedProperty data, string DBConnection) //以ADO完成
    {
      
      using (SqlConnection conn = new SqlConnection(DBConnection))
      {
        try
        {
          conn.Open();
          data.target = data.target == "Description" ? "MS_Description" : data.target;
          string strcommand = data.column == "" ?
              $"EXEC sys.sp_updateextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table" :
              $"EXEC sys.sp_updateextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table, @level2type=N'COLUMN',@level2name=@data_column";
          SqlCommand command = new SqlCommand(strcommand, conn);
          command.Parameters.Add(new SqlParameter("@data_target", data.target));
          command.Parameters.Add(new SqlParameter("@data_value", data.value));
          command.Parameters.Add(new SqlParameter("@data_table", data.table));
          command.Parameters.Add(new SqlParameter("@data_column", data.column));

          command.ExecuteNonQuery();

        }
        catch (SqlException e)
        {
          Console.WriteLine(e);
          //如果還未建立該擴充屬性，就改成新增擴充屬性
          string strcommand = data.column == "" ?
              $"EXEC sys.sp_addextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table" :
              $"EXEC sys.sp_addextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table, @level2type=N'COLUMN',@level2name=@data_column";
          SqlCommand command = new SqlCommand(strcommand, conn);
          command.Parameters.Add(new SqlParameter("@data_target", data.target));
          command.Parameters.Add(new SqlParameter("@data_value", data.value));
          command.Parameters.Add(new SqlParameter("@data_table", data.table));
          command.Parameters.Add(new SqlParameter("@data_column", data.column));

          command.ExecuteNonQuery();
        }
        finally
        {
          conn.Close();
        }
      }
    }
  }
}
