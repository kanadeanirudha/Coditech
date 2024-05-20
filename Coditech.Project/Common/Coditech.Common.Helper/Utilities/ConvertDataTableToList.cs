using System.Data;
using System.Reflection;

namespace Coditech.Common.Helper.Utilities
{
    public class ConvertDataTableToList
    {
        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                list.Add(item);
            }

            return list;
        }

        public T GetItem<T>(DataRow dr)
        {
            Type typeFromHandle = typeof(T);
            T val = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                PropertyInfo[] properties = typeFromHandle.GetProperties();
                foreach (PropertyInfo propertyInfo in properties)
                {
                    if (propertyInfo.Name == column.ColumnName)
                    {
                        object obj = dr[column.ColumnName];
                        if (obj == DBNull.Value)
                        {
                            obj = null;
                        }

                        propertyInfo.SetValue(val, obj, null);
                    }
                }
            }

            return val;
        }
    }
}
