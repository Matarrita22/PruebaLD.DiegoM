using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PruebaLD.DiegoM.Utils
{
    public class ExceptionLog
    {
        /// <summary>
        /// Se encarga de insertar los errores en la tabla de erroreslog
        /// </summary>
        /// <param name="pClase"></param>
        /// <param name="pMetodo"></param>
        /// <param name="pMsgDetallado"></param>
        /// <param name="pConnection"></param>
        public static void InsertarBitacoraErrores(string pClase, string pMetodo, string pMsgDetallado, string pConnection)
        {
            IDbConnection Conn = new SqlConnection(pConnection);
            try
            {
                Conn.Open();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@clase", pClase);
                queryParameters.Add("@metodo", pMetodo);
                queryParameters.Add("@mensajeError", pMsgDetallado);

                var Query = Conn.Execute("usp_InsertaLog", queryParameters, commandType: CommandType.StoredProcedure);
                Conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
