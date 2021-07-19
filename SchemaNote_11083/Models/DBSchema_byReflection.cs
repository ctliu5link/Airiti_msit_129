using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Models
{
  public class DBSchema_byReflection : DBSchema_Base
  {
    public override IEnumerable<Ttable> getSchema(string DBConnection)
    {
      IEnumerable<Ttable> Tables = null;
      try
      {
        using (var conn = new SqlConnection(DBConnection))
        {
          SqlCommand commandTables = new SqlCommand(SQLCommand.dapper_getAllTables(), conn);
          DataSet ds = new DataSet();
          SqlDataAdapter da = new SqlDataAdapter(commandTables);
          da.Fill(ds);

          Tables = ds.Tables[0].DataMapping<Ttable>();
          foreach (var table in Tables)
          {
            SqlCommand command = new SqlCommand(SQLCommand.dapper_getTableColumns(), conn);
            command.Parameters.Add(new SqlParameter("@Table_Name", table.Table_Name));
            ds = new DataSet();
            da = new SqlDataAdapter(command);
            da.Fill(ds);
            table.TtableColumns = ds.Tables[0].DataMapping<TtableColumn>();
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return null;
      }
      return Tables;
    }
  }
}
