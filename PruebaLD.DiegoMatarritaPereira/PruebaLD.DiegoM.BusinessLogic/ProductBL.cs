using PruebaLD.DiegoM.DataAccess;
using PruebaLD.DiegoM.Entities;
using PruebaLD.DiegoM.Utils;
using System;
using System.Collections.Generic;

namespace PruebaLD.DiegoM.BusinessLogic
{
    public class ProductBL
    {
        /// <summary>
        /// Se encarga de obtener todos los articulos, llama metodo de la capa datos
        /// </summary>
        /// <param name="pConecctionDB"></param>
        /// <returns></returns>
        public IEnumerable<ProductEntitie> ObtenerArticulo(string pConecctionDB)
        {
            try
            {
                ProductDA productDA = new ProductDA();

                return productDA.ObtenerArticulo(pConecctionDB);
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(ProductBL), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConecctionDB);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el idArticulo
        /// </summary>
        /// <param name="pArticulo"></param>
        /// <param name="pConecctionDB"></param>
        /// <returns></returns>
        public IEnumerable<ProductEntitie> ObtenerArticuloXId(int pArticulo, string pConecctionDB)
        {
            try
            {
                ProductDA productDA = new ProductDA();

                return productDA.ObtenerArticuloXId(pArticulo, pConecctionDB);
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(ProductBL), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConecctionDB);
                throw;
            }
        }

        /// <summary>
        /// Se encarga de agregar los articulos, llama metodo de la capa datos
        /// </summary>
        /// <param name="pArticulo"></param>
        /// <param name="pConecctionDB"></param>
        /// <returns></returns>
        public int AgregarArticulo(ProductEntitie pArticulo, string pConecctionDB)
        {
            try
            {
                ProductDA productDA = new ProductDA();

                return productDA.AgregarArticulo(pArticulo, pConecctionDB);
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(ProductBL), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConecctionDB);
                throw;
            }
        }

        /// <summary>
        /// Se encarga de actualizar los articulos por id, llama metodo de la capa datos
        /// </summary>
        /// <param name="pArticulo"></param>
        /// <param name="pConecctionDB"></param>
        /// <returns></returns>
        public int ActualizarArticulo(ProductEntitie pArticulo, string pConecctionDB)
        {
            try
            {
                ProductDA productDA = new ProductDA();

                return productDA.ActualizarArticulo(pArticulo, pConecctionDB);
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(ProductBL), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConecctionDB);
                throw;
            }
        }

        /// <summary>
        /// Se encarga de eliminar los articulos por id, llama metodo de la capa datos
        /// </summary>
        /// <param name="pArticulo"></param>
        /// <param name="pConecctionDB"></param>
        /// <returns></returns>
        public int EliminarArticulo(ProductEntitie pArticulo, string pConecctionDB)
        {
            try
            {
                ProductDA productDA = new ProductDA();
                if (pArticulo.IdArticulo != 0 || pConecctionDB != "")
                {
                    return productDA.EliminarArticulo(pArticulo.IdArticulo, pConecctionDB);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(ProductBL), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConecctionDB);
                throw;
            }
        }

        /// <summary>
        /// Genera compra
        /// </summary>
        /// <param name="pInvoice"></param>
        /// <param name="pConecctionDB"></param>
        /// <returns></returns>
        public int GenerarCompra(List<InvoiceCustomer> pInvoice, string pConecctionDB)
        {
            try
            {
                ProductDA consultasBD = new ProductDA();

                return consultasBD.GenerarCompra(DataTableUtilities.ConvertirADataTable(pInvoice), pConecctionDB);

            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(ProductBL), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConecctionDB);
                throw;
            }
        }
    }
}
