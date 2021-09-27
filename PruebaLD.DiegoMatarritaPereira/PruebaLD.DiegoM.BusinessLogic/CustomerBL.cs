using PruebaLD.DiegoM.DataAccess;
using PruebaLD.DiegoM.Entities;
using PruebaLD.DiegoM.Utils;
using System;
using System.Collections.Generic;

namespace PruebaLD.DiegoM.BusinessLogic
{
    public class CustomerBL
    {
        #region Metodos Publicos

        /// <summary>
        /// Metodo logico para llenar clientes
        /// </summary>
        /// <param name="pConecctionDB"></param>
        /// <returns></returns>
        public IEnumerable<CustomerEntitie> ObtenerClientes(string pConecctionDB)
        {
            try
            {
                CustomerDA customerDA = new CustomerDA();
                return customerDA.ObtenerClienteInfo(pConecctionDB);
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(CustomerBL), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConecctionDB);
                throw;
            }
        }

        /// <summary>
        /// Metodo logico para obtener info clientes por nombre
        /// </summary>
        /// <param name="pConecctionDB"></param>
        /// <param name="pNombre"></param>
        /// <returns></returns>
        public IEnumerable<CustomerEntitie> ObtenerClientesXNombre(string pConecctionDB, string pNombre)
        {
            try
            {
                CustomerDA customerDA = new CustomerDA();

                return customerDA.ObtenerClienteInfoXNombre(pConecctionDB, pNombre);
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(CustomerBL), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConecctionDB);
                throw;
            }
        }

        /// <summary>
        /// Obtiene clientes x cedula
        /// </summary>
        /// <param name="pConecctionDB"></param>
        /// <param name="pCedula"></param>
        /// <returns></returns>
        public IEnumerable<CustomerEntitie> ObtenerClientesXCedula(string pConecctionDB, string pCedula)
        {
            try
            {
                CustomerDA customerDA = new CustomerDA();

                return customerDA.ObtenerClienteInfoXCedula(pConecctionDB, pCedula);
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(CustomerBL), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConecctionDB);
                throw;
            }
        }

        /// <summary>
        /// Inserta los clientes 
        /// </summary>
        /// <param name="pCliente"></param>
        /// <param name="pConecctionDB"></param>
        /// <returns></returns>
        public int AgregarInfoCliente(CustomerEntitie pCliente, string pConecctionDB)
        {
            try
            {
                CustomerDA customerDA = new CustomerDA();

                return customerDA.AgregarCliente(pCliente, pConecctionDB);
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(CustomerBL), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConecctionDB);
                throw;
            }
        }

        /// <summary>
        /// Actualiza el cliente llama metodo de la capa datos
        /// </summary>
        /// <param name="pCliente"></param>
        /// <param name="pConecctionDB"></param>
        /// <returns></returns>
        public int ActualizarCliente(CustomerEntitie pCliente, string pConecctionDB)
        {
            try
            {
                CustomerDA customerDA = new CustomerDA();

                return customerDA.ActualizarCliente(pCliente, pConecctionDB);
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(CustomerBL), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConecctionDB);
                throw;
            }
        }

        /// <summary>
        /// Elimina cliente, llama metodo capa datos
        /// </summary>
        /// <param name="pCustomer"></param>
        /// <param name="pConecctionDB"></param>
        /// <returns></returns>
        public string EliminarCliente(CustomerEntitie pCustomer, string pConecctionDB)
        {
            try
            {
                CustomerDA customerDA = new CustomerDA();
                if (pCustomer.CedulaCliente != null || pCustomer.CedulaCliente != "" || pConecctionDB != "")
                {
                    return customerDA.EliminarCliente(pCustomer.CedulaCliente, pConecctionDB);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.InsertarBitacoraErrores(nameof(CustomerBL), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, pConecctionDB);
                throw;
            }
        }

        #endregion
    }
}
