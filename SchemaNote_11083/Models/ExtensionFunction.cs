using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Models
{

    public static class ExtensionFunction
    {
        public static List<T> DataMapping<T>(this DataTable table)
        {

            List<T> tables = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                var newTable = Activator.CreateInstance<T>();
                foreach (var property in typeof(T).GetProperties())
                {
                    switch (property.PropertyType.Name)
                    {
                        case "String":
                            property.SetValue(newTable, row[property.Name].ToString());
                            break;
                        case "Int32":
                            property.SetValue(newTable, (int)row[property.Name]);
                            break;
                        case "Int64":
                            property.SetValue(newTable, (long)row[property.Name]);
                            break;
                        default:
                            break;
                    }
                }
                tables.Add(newTable);
            }
            return tables;
        }

    }
}
