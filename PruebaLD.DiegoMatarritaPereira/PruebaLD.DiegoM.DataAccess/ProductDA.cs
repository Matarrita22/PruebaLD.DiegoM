using Dapper;
using PruebaLD.DiegoM.Entities;
using PruebaLD.DiegoM.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PruebaLD.DiegoM.DataAccess
{
    public class ProductDA
    {
        /// <summary>
        /// Obtiene la informacion del articulo
        /// </summary>
        /// <param name="pConnection"></param>
        /// <returns></returns>
        public IEnumerable<ProductEntitie> ObtenerArticulo(string pConnection)
        {
            IDbConnection Conn = new SqlConnection(pConnection);
            try
            {
                // Abre la conexion
                Conn.Open();
                // Ejecuta el stored procedure
                var Query = Conn.Query<ProductEntitie>("usp_ObtieneArticulo");
                // Cierra conexion
                Conn.Close();
                // Retorna resultados
                return Query;
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(ProductDA), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConnection);
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene los articulos x id
        /// </summary>
        /// <param name="pIdArticulo"></param>
        /// <param name="pConnection"></param>
        /// <returns></returns>
        public IEnumerable<ProductEntitie> ObtenerArticuloXId(int pIdArticulo, string pConnection)
        {
            IDbConnection Conn = new SqlConnection(pConnection);
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@idArticulo", pIdArticulo);
                // Abre la conexion
                Conn.Open();
                // Ejecuta el stored procedure
                var Query = Conn.Query<ProductEntitie>("usp_ObtieneArticuloXId", queryParameters, commandType: CommandType.StoredProcedure);
                // Cierra conexion
                Conn.Close();
                // Retorna resultados
                return Query;
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(ProductDA), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConnection);
                throw ex;
            }
        }

        /// <summary>
        /// Agregar informacion del articulo
        /// </summary>
        /// <param name="pArticulo"></param>
        /// <param name="pConnection"></param>
        /// <returns></returns>
        public int AgregarArticulo(ProductEntitie pArticulo, string pConnection)
        {
            IDbConnection Conn = new SqlConnection(pConnection);
            try
            {
                Conn.Open();

                var queryParameters = new DynamicParameters();
                queryParameters.Add("@nombreArticulo", pArticulo.NombreArticulo);
                queryParameters.Add("@precioArticulo", pArticulo.PrecioArticulo);
                queryParameters.Add("@descripcionArticulo", pArticulo.DescripcionArticulo);

                // Ejecuta el stored procedure
                var Query = Conn.Execute("usp_InsertaArticulo", queryParameters, commandType: CommandType.StoredProcedure);
                // Cierra conexion
                Conn.Close();
                return Query;
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(ProductDA), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConnection);
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza la informacion del articulo por idarticulo
        /// </summary>
        /// <param name="pArticuloEntidad"></param>
        /// <param name="pConnection"></param>
        /// <returns></returns>
        public int ActualizarArticulo(ProductEntitie pArticuloEntidad, string pConnection)
        {
            IDbConnection Conn = new SqlConnection(pConnection);
            try
            {
                Conn.Open();

                var queryParameters = new DynamicParameters();
                queryParameters.Add("@idArticulo", pArticuloEntidad.IdArticulo);
                queryParameters.Add("@nombreArticulo", pArticuloEntidad.NombreArticulo);
                queryParameters.Add("@precioArticulo", pArticuloEntidad.PrecioArticulo);
                queryParameters.Add("@descripcionArticulo", pArticuloEntidad.DescripcionArticulo);

                // Ejecuta el stored procedure
                var Query = Conn.Execute("usp_ActualizaArticulo", queryParameters, commandType: CommandType.StoredProcedure);
                // Cierra conexion
                Conn.Close();
                return Query;
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(ProductDA), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConnection);
                throw ex;
            }
        }

        /// <summary>
        /// Elimina la informacion del articulo
        /// </summary>
        /// <param name="pIdArticulo"></param>
        /// <param name="pConnection"></param>
        /// <returns></returns>
        public int EliminarArticulo(int pIdArticulo, string pConnection)
        {
            IDbConnection Conn = new SqlConnection(pConnection);
            try
            {
                Conn.Open();

                var queryParameters = new DynamicParameters();
                queryParameters.Add("@IdArticulo", pIdArticulo);
                // Ejecuta el stored procedure
                var Query = Conn.Execute("usp_EliminaArticulo", queryParameters, commandType: CommandType.StoredProcedure);
                // Cierra conexion
                Conn.Close();
                return Query;
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(ProductDA), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConnection);
                throw ex;
            }
        }

        /// <summary>
        /// Inserta la factura generada
        /// </summary>
        /// <param name="pFactura"></param>
        /// <param name="pConnection"></param>
        /// <returns></returns>
        public int GenerarCompra(DataTable pFactura, string pConnection)
        {
            int resultado = 0;
            IDbConnection Conn = new SqlConnection(pConnection);
            try
            {
                using (SqlConnection db = new SqlConnection(pConnection))
                {
                    db.Open();

                    SqlCommand cmd = new SqlCommand("usp_InsertaFactura", db)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "TypeFE",
                        SqlDbType = SqlDbType.Structured,
                        Value = pFactura
                    };
                    cmd.Parameters.Add(param);

                    resultado = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(ProductDA), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConnection);
                resultado = 0;
            }
            return resultado;
        }
    }
}
