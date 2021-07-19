using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Models.Interface
{
  /// <summary>
  /// 因使用不同技術對資料庫進行存取，固定義統一介面做使用。
  /// </summary>
    interface IDBSchema
    {
    public IEnumerable<Ttable> getSchema(string DBConnection);
    public void updateExpendedProperty(tUpdateExpendedProperty data, string DBConnection);

    }
}
