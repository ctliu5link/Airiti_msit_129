using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Models
{

    public static class ExtensionFunction
    {
        public static List<T> DataMapping<T>(this DataTable p_table)
        {

            List<T> Table = new List<T>();

            foreach (DataRow row in p_table.Rows)
            {
                var newRow = Activator.CreateInstance<T>();
                foreach (var property in typeof(T).GetProperties())
                {
                    switch (property.PropertyType.Name)
                    {
                        case "String":
                            property.SetValue(newRow, row[property.Name].ToString());
                            break;
                        case "Int32":
                            property.SetValue(newRow, (int)row[property.Name]);
                            break;
                        case "Int64":
                            property.SetValue(newRow, (long)row[property.Name]);
                            break;
                        default:
                            break;
                    }
                }
                Table.Add(newRow);
            }
            return Table;
        }

    }
}
