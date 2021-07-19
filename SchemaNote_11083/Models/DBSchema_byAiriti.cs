using Airiti.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Models
{
  public class DBSchema_byAiriti : DBSchema_Base
  {
    public override IEnumerable<Ttable> getSchema(string DBConnection)
    {
      IEnumerable<Ttable> Tables = null;
      try
      {
        Tables = DBService.SingleQuery(DBConnection, SQLCommand.dapper_getAllTables())
                    .ReturnData.DataMapping<Ttable>();

        foreach (var table in Tables)
        {
          table.TtableColumns = DBService.SingleQuery(
              DBConnection,
              SQLCommand.dapper_getTableColumns(),
              new List<QueryField>
          {
                        new QueryField("@Table_Name",table.Table_Name)
          }).ReturnData.DataMapping<TtableColumn>();
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
      try
      {
        data.target = data.target == "Description" ? "MS_Description" : data.target;
        string strcommand = data.column == "" ?
            $"EXEC sys.sp_updateextendedproperty @name={data.target}, @value={data.value} , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name={data.table}" :
            $"EXEC sys.sp_updateextendedproperty @name={data.target}, @value={data.value} , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name={data.table}, @level2type=N'COLUMN',@level2name={data.column}";
        DBService.MutiNonQuery(DBConnection, strcommand);
      }
      catch (SqlException e)
      {
        Console.WriteLine(e);
        //如果還未建立該擴充屬性，就改成新增擴充屬性
        string strcommand = data.column == "" ?
            $"EXEC sys.sp_addextendedproperty @name={data.target}, @value={data.value} , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name={data.table}" :
            $"EXEC sys.sp_addextendedproperty @name={data.target}, @value={data.value} , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name={data.table}, @level2type=N'COLUMN',@level2name={data.column}";
        DBService.MutiNonQuery(DBConnection, strcommand);
      }
    }

  }
}

