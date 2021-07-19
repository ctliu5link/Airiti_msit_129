using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Models
{
  public class DBSchema_byDapper : DBSchema_Base
  {
    public override IEnumerable<Ttable> getSchema(string DBConnection)
    {
      IEnumerable<Ttable> Tables = null;
      try
      {
        using (var conn = new SqlConnection(DBConnection))
        {
          Tables = conn.Query<Ttable>(SQLCommand.dapper_getAllTables());
          foreach (var table in Tables)
          {
            table.TtableColumns =
                conn.Query<TtableColumn>(SQLCommand.dapper_getTableColumns(), new { Table_Name = table.Table_Name });
          }
        }
      }
      catch
      {
        return null;
      }
      return Tables;
    }

    public override void updateExpendedProperty(tUpdateExpendedProperty data, string DBConnection)
    {

      using (SqlConnection conn = new SqlConnection(DBConnection))
      {
        try
        {
          data.target = data.target == "Description" ? "MS_Description" : data.target;
          string strcommand = data.column == "" ?
              $"EXEC sys.sp_updateextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table" :
              $"EXEC sys.sp_updateextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table, @level2type=N'COLUMN',@level2name=@data_column";
          conn.Execute(strcommand, new
          {
            data_target = data.target,
            data_value = data.value,
            data_table = data.table,
            data_column = data.column
          });
        }
        catch (SqlException e)
        {
          Console.WriteLine(e);
          //如果還未建立該擴充屬性，就改成新增擴充屬性
          string strcommand = data.column == "" ?
              $"EXEC sys.sp_addextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table" :
              $"EXEC sys.sp_addextendedproperty @name=@data_target, @value=@data_value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@data_table, @level2type=N'COLUMN',@level2name=@data_column";
          conn.Execute(strcommand, new
          {
            data_target = data.target,
            data_value = data.value,
            data_table = data.table,
            data_column = data.column
          });
        }
      }
    }
  }

 
}
