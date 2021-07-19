using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Models
{
  public class DBSchma_byADO : DBSchema_Base
  {
    public override IEnumerable<Ttable> getSchema(string DBConnection)
    {
      IEnumerable<Ttable> Tables = null;
      SqlConnection conn = null;
      try
      {
        using (conn = new SqlConnection(DBConnection))
        {
          var adapter = new SqlDataAdapter(SQLCommand.schemaCommand(), conn);
          var dataset = new DataSet();
          adapter.Fill(dataset);

          var dtRows = dataset.Tables[0]; //所有欄位資料
          var dtTable = dataset.Tables[1]; //所有table資料

          Tables = new Ttable[dtTable.Rows.Count];
          int tableCount = 0;

          foreach (DataRow item in dtTable.Rows)
          {
            Ttable newTable = new Ttable
            {
              Table_Name = item["Table_Name"].ToString(),
              TtableColumns = new TtableColumn[(int)item["Column_qty"]]
            };

            ((Ttable[])Tables)[tableCount] = newTable;
            tableCount++;
          }

          Ttable currentTable = null;

          int locationCount = 0;
          foreach (DataRow item in dataset.Tables[0].Rows)
          {
            if (currentTable == null || item["Table_Name"].ToString() != currentTable.Table_Name)
            {
              locationCount = 0;
              currentTable = Tables.FirstOrDefault(n => n.Table_Name == item["Table_Name"].ToString());
              currentTable.Table_Description = item["Table_Description"].ToString();
              currentTable.Table_REMARK = item["Table_REMARK"].ToString();
              currentTable.Table_Schema = item["Table_Schema"].ToString();
              currentTable.Create_Date = item["Create_Date"].ToString();
              currentTable.Modify_Date = item["Modify_Date"].ToString();
              currentTable.Total_Rows = (long)item["Total_Rows"];
            }

            TtableColumn newColumn = new TtableColumn
            {
              Column_Default = item["Column_Default"].ToString(),
              Column_Name = item["Column_Name"].ToString(),
              Data_Type = item["Data_Type"].ToString(),
              Description = item["Description"].ToString(),
              IS_Nullable = item["IS_Nullable"].ToString() == "YES" ? 1 : 0,
              PK = item["PK"].ToString() == "" ? 0 : 1,
              REMARK = item["REMARK"].ToString()
            };
            ((TtableColumn[])currentTable.TtableColumns)[locationCount] = newColumn;
            locationCount++;
          }
        }
      }
      catch (Exception e)
      {
        return null;
      }
      finally
      {
        conn.Close();
      }
      return Tables;
    }
  }
}
