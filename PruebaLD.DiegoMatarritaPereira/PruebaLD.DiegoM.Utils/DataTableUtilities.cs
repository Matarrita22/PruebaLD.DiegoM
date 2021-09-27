using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace PruebaLD.DiegoM.Utils
{
    public static class DataTableUtilities
    {
        /// <summary>
        /// Clase Generica que convierte de lista a datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pLista"></param>
        /// <returns></returns>
        public static DataTable ConvertirADataTable<T>(this IEnumerable<T> pLista)
        {
            try
            {
                PropertyDescriptorCollection propiedades = TypeDescriptor.GetProperties(typeof(T));
                DataTable vloDataTable = new DataTable();
                foreach (PropertyDescriptor propiedad in propiedades)
                    vloDataTable.Columns.Add(propiedad.Name, Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType);
                foreach (T item in pLista)
                {
                    DataRow vloDataRow = vloDataTable.NewRow();
                    foreach (PropertyDescriptor vloPropertyDescriptor in propiedades)
                        vloDataRow[vloPropertyDescriptor.Name] = vloPropertyDescriptor.GetValue(item) ?? DBNull.Value;
                    vloDataTable.Rows.Add(vloDataRow);
                }
                return vloDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
