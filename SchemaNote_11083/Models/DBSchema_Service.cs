
using Airiti.DataAccess;
using Dapper;
using Microsoft.AspNetCore.Http;
using SchemaNote_11083.Models.Interface;
using SchemaNote_11083.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace SchemaNote_11083.Models
{
  public class DBSchema_Service
  {

    private readonly Dictionary<string, IDBSchema> MethodList = new Dictionary<string, IDBSchema>()
    {
      { "ADO",new DBSchma_byADO() },
      { "Reflection",new DBSchema_byReflection() },
      { "Airiti",new DBSchema_byReflection() },
      { "Dapper",new DBSchema_byDapper() }
    };
    #region 取得DBSchema資料
    public TtableSchema<IEnumerable<Ttable>> getDBSchema(string DBConnection, string Method)
    {
      IEnumerable<Ttable> tables = MethodList[Method].getSchema(DBConnection);

      return tables.ToList().Count() == 0 || tables ==  null ?
         new TtableSchema<IEnumerable<Ttable>>() : new TtableSchema<IEnumerable<Ttable>>(tables,Method);
    }



    #endregion
    #region 更新擴充屬性資料
    public void updateExpendedProperty(tUpdateExpendedProperty data, string DBConnection)
    {
      MethodList[data.method1].updateExpendedProperty(data,DBConnection);
    }

    #endregion

  }

}
