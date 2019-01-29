using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EohiQuartzService.Quarzt
{

    public static class DataTableEntityConverter
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static T ConvertToEntity<T>(DataRow tableRow) where T : new()
        {
            // Create a new type of the entity I want
            Type t = typeof(T);
            T returnObject = new T();

            foreach (DataColumn col in tableRow.Table.Columns)
            {
                string colName = col.ColumnName;

                // Look for the object's property with the columns name, ignore case
                PropertyInfo pInfo = t.GetProperty(colName.ToLower(),
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // did we find the property ?
                if (pInfo != null)
                {
                    object val = tableRow[colName];

                    // is this a Nullable<> type
                    bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);
                    if (IsNullable)
                    {
                        if (val is System.DBNull)
                        {
                            val = null;
                        }
                        else
                        {
                            // Convert the db type into the T we have in our Nullable<T> type
                            val = Convert.ChangeType(val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                        }
                    }
                    else
                    {
                        try
                        {
                            // Convert the db type into the type of the property in our entity
                            val = Convert.ChangeType(val, pInfo.PropertyType);
                        }
                        catch (Exception ex)
                        {
                            string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), "DataTableEntityConverter" + ex.Message);
                            log.Info(start);
                        }

                    }
                    try
                    {
                        // Set the value of the property with the value from the db
                        pInfo.SetValue(returnObject, val, null);
                    }
                    catch (Exception ex)
                    {
                        string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), "DataTableEntityConverter+ pInfo" + ex.Message);
                        log.Info(start);
                    }
                }
            }

            // return the entity object with values
            return returnObject;
        }
        public static List<T> ConvertToEntityList<T>(DataTable table) where T : new()
        {
            // Create a new type of the entity I want
            List<T> result = new List<T>();

            foreach (DataRow tableRow in table.Rows)
            {
                Type t = typeof(T);
                T returnObject = new T();
                foreach (DataColumn col in table.Columns)
                {
                    string colName = col.ColumnName;

                    // Look for the object's property with the columns name, ignore case
                    PropertyInfo pInfo = t.GetProperty(colName.ToLower(),
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    // did we find the property ?
                    if (pInfo != null)
                    {
                        object val = tableRow[colName];

                        // is this a Nullable<> type
                        bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);
                        if (IsNullable)
                        {
                            if (val is System.DBNull)
                            {
                                val = null;
                            }
                            else
                            {
                                // Convert the db type into the T we have in our Nullable<T> type
                                val = Convert.ChangeType
                        (val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                            }
                        }
                        else
                        {
                            // Convert the db type into the type of the property in our entity
                            val = Convert.ChangeType(val, pInfo.PropertyType);
                        }
                        // Set the value of the property with the value from the db
                        pInfo.SetValue(returnObject, val, null);
                    }
                }
                result.Add(returnObject);
            }

            // return the entity object with values
            return result;
        }

        public static DataTable GetDataTableSchema<T>()
        {
            PropertyDescriptorCollection props =
                           TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                Type pt = prop.PropertyType;
                if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                    pt = Nullable.GetUnderlyingType(pt);
                table.Columns.Add(prop.Name, pt);
            }
            return table;
        }

        public static DataTable ConvertToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection props =
                           TypeDescriptor.GetProperties(typeof(T));
            DataTable table = GetDataTableSchema<T>();
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            table.AcceptChanges();
            return table;
        }

    }
}
