using Dapper;
using PruebaLD.DiegoM.Entities;
using PruebaLD.DiegoM.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PruebaLD.DiegoM.DataAccess
{
    public class CustomerDA
    {

        /// <summary>
        /// Obtiene todos los clientes 
        /// </summary>
        /// <param name="pConection"></param> Parametro que lleva la conexion de la base de datos
        /// <returns></returns>
        public IEnumerable<CustomerEntitie> ObtenerClienteInfo(string pConection)
        {
            IDbConnection Conn = new SqlConnection(pConection);
            try
            {
                // Abre la conexion
                Conn.Open();
                // Ejecuta el stored procedure
                var Query = Conn.Query<CustomerEntitie>("usp_ObtieneClientes");
                // Cierra conexion
                Conn.Close();
                // Retorna resultados
                return Query;
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(CustomerDA), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConection);
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene los datos del cliente por nombre, esto para ser filtrado en el front end
        /// </summary>
        /// <param name="pConnection"></param> Conexion a BD
        /// <param name="pNombre"></param> Parametro nomnre
        /// <returns></returns>
        public IEnumerable<CustomerEntitie> ObtenerClienteInfoXNombre(string pConnection, string pNombre)
        {
            IDbConnection Conn = new SqlConnection(pConnection);
            try
            {
                // Abre la conexion
                Conn.Open();

                // Es necesario agregar los parametros para el uso correcto de los stored procedure
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@nombreCliente", pNombre);

                // Ejecuta el stored procedure
                var Query = Conn.Query<CustomerEntitie>("usp_ObtieneClientesXNombre", queryParameters, commandType: CommandType.StoredProcedure);
                // Cierra conexion
                Conn.Close();
                // Retorna resultados
                return Query;
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(CustomerDA), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConnection);
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la info del cliente por cedula
        /// </summary>
        /// <param name="pConnection"></param>
        /// <param name="pCedula"></param>
        /// <returns></returns>
        public IEnumerable<CustomerEntitie> ObtenerClienteInfoXCedula(string pConnection, string pCedula)
        {
            IDbConnection Conn = new SqlConnection(pConnection);
            try
            {
                // Abre la conexion
                Conn.Open();

                // Es necesario agregar los parametros para el uso correcto de los stored procedure
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@cedulaCliente", pCedula);

                // Ejecuta el stored procedure
                var Query = Conn.Query<CustomerEntitie>("usp_ObtieneClientesXCedula", queryParameters, commandType: CommandType.StoredProcedure);
                // Cierra conexion
                Conn.Close();
                // Retorna resultados
                return Query;
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(CustomerDA), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConnection);
                throw ex;
            }
        }

        /// <summary>
        /// Agregar informacion del cliente
        /// </summary>
        /// <param name="pCliente"></param>
        /// <param name="pConnection"></param>
        /// <returns></returns>
        public int AgregarCliente(CustomerEntitie pCliente, string pConnection)
        {
            IDbConnection Conn = new SqlConnection(pConnection);
            try
            {
                Conn.Open();

                var queryParameters = new DynamicParameters();
                queryParameters.Add("@cedulaCliente", pCliente.CedulaCliente);
                queryParameters.Add("@nombreCliente", pCliente.NombreCliente);
                queryParameters.Add("@apellidoCliente", pCliente.Apellido);

                // Ejecuta el stored procedure
                var Query = Conn.Execute("usp_InsertaCliente", queryParameters, commandType: CommandType.StoredProcedure);
                // Cierra conexion
                Conn.Close();
                return Query;
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(CustomerDA), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConnection);
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza la informacion del cliente por cedula
        /// </summary>
        /// <param name="pCliente"></param>
        /// <param name="pConnection"></param>
        /// <returns></returns>
        public int ActualizarCliente(CustomerEntitie pCliente, string pConnection)
        {
            IDbConnection Conn = new SqlConnection(pConnection);
            try
            {
                Conn.Open();

                var queryParameters = new DynamicParameters();
                queryParameters.Add("@cedulaCliente", pCliente.CedulaCliente);
                queryParameters.Add("@nombreCliente", pCliente.NombreCliente);
                queryParameters.Add("@apellidoCliente", pCliente.Apellido);

                // Ejecuta el stored procedure
                var Query = Conn.Execute("usp_ActualizaCliente", queryParameters, commandType: CommandType.StoredProcedure);
                // Cierra conexion
                Conn.Close();
                return Query;
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(CustomerDA), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConnection);
                throw ex;
            }
        }

        /// <summary>
        /// Elimina la informacion del cliente
        /// </summary>
        /// <param name="pCedula"></param>
        /// <param name="pConnection"></param>
        /// <returns></returns>
        public string EliminarCliente(string pCedula, string pConnection)
        {
            IDbConnection Conn = new SqlConnection(pConnection);
            try
            {
                Conn.Open();

                var queryParameters = new DynamicParameters();
                queryParameters.Add("@cedulaCliente", pCedula);
                // Ejecuta el stored procedure
                var Query = Conn.ExecuteScalar<string>("usp_EliminaCliente", queryParameters, commandType: CommandType.StoredProcedure);
                // Cierra conexion
                Conn.Close();
                return Query.ToString();
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(CustomerDA), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConnection);
                throw ex;
            }
        }
    }
}
