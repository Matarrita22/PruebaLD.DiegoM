using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PruebaLD.DiegoM.BusinessLogic;
using PruebaLD.DiegoM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PruebaLD.DiegoM.API.Controllers
{

    [Route("api/[controller]")]//se configura en ApiWEB\Properties\launchSettings.json para establecer el "arranque"
    [ApiController]

    public class CustomerController : ControllerBase
    {
        #region BDConfiguracion

        private readonly IConfiguration _configuration;
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        #endregion

        #region Clientes

        #region Metodos Publicos

        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObtenerClientes")]//Acceder a la ruta del metodo.
        public IEnumerable<CustomerEntitie> ObtieneClientes()
        {
            HttpResponseMessage APIResponse;
            List<CustomerEntitie> CustomerList = null;
            try
            {
                string DBConnection = _configuration.GetSection("ConnectionStrings").GetSection("conBD").Value; //Leer valor del settings.json
                return ObtieneClientesInfo(DBConnection).ToArray();
            }
            catch (Exception)
            {
                APIResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return CustomerList;
            }
        }

        /// <summary>
        /// Metodo que obtiene la informacion del cliente por cedula
        /// </summary>
        /// <param name="pCustomer"></param>
        /// <returns></returns>
        [HttpGet, HttpPost]
        [Route("ObtenerClientesXCedula")]//Acceder a la ruta del metodo.
        public IEnumerable<CustomerEntitie> ObtieneClientesXCedula(CustomerEntitie pCustomer)
        {
            HttpResponseMessage APIResponse;
            List<CustomerEntitie> CustomerList = null;
            try
            {
                string DBConnection = _configuration.GetSection("ConnectionStrings").GetSection("conBD").Value; //Leer valor del settings.json
                return ObtieneClientesXCedula(DBConnection, pCustomer.CedulaCliente).ToArray();
            }
            catch (Exception)
            {
                APIResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return CustomerList;
            }
        }

        /// <summary>
        /// Obtiene los clientes X Nombre
        /// </summary>
        /// <param name="pNombre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObtenerClientesXNombre")]//Acceder a la ruta del metodo.
        public IEnumerable<CustomerEntitie> ObtieneClientesXNombre(string pNombre)
        {
            HttpResponseMessage APIResponse;
            List<CustomerEntitie> CustomerList = null;
            try
            {
                string DBConnection = _configuration.GetSection("ConnectionStrings").GetSection("conBD").Value; //Leer valor del settings.json
                return ObtieneClientesXNombre(DBConnection, pNombre).ToArray();
            }
            catch (Exception)
            {
                APIResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return CustomerList;
            }
        }

        /// <summary>
        /// Agrega la informacion de los clientes
        /// </summary>
        /// <param name="pCustomer"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AgregarCliente")]//Acceder a la ruta del metodo.
        public ActionResult AgregarCliente(CustomerEntitie pCustomer)
        {
            string databaseConnection = _configuration.GetSection("ConnectionStrings").GetSection("conBD").Value; //Leer valor del settings.json
            CustomerBL customerBL = new CustomerBL();
            string result = String.Empty;
            ActionResult APIResponse;
            try
            {
                result = customerBL.AgregarInfoCliente(pCustomer, databaseConnection).ToString();
                APIResponse = Ok(result);
            }
            catch (Exception)
            {
                APIResponse = BadRequest();
            }
            return APIResponse;
        }

        /// <summary>
        /// Actualiza la informacion de los clientes
        /// </summary>
        /// <param name="pCustomer"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ActualizarCliente")]//Acceder a la ruta del metodo.
        public ActionResult ActualizarCliente(CustomerEntitie pCustomer)
        {
            string databaseConnection = _configuration.GetSection("ConnectionStrings").GetSection("conBD").Value; //Leer valor del settings.json
            CustomerBL customerBL = new CustomerBL();
            string result = String.Empty;
            ActionResult APIResponse;
            try
            {
                result = customerBL.ActualizarCliente(pCustomer, databaseConnection).ToString();
                APIResponse = Ok(result);
            }
            catch (Exception)
            {
                APIResponse = BadRequest();
            }
            return APIResponse;
        }

        /// <summary>
        /// Elimina la informacion de los clientes
        /// </summary>
        /// <param name="pCustomer"></param>
        /// <returns></returns>
        [HttpPost, HttpDelete]
        [Route("EliminarCliente")]//Acceder a la ruta del metodo.
        public ActionResult EliminarCliente(CustomerEntitie pCustomer)
        {
            string databaseConnection = _configuration.GetSection("ConnectionStrings").GetSection("conBD").Value; //Leer valor del settings.json
            CustomerBL customerBL = new CustomerBL();
            string result = String.Empty;
            ActionResult APIResponse;
            try
            {
                result = customerBL.EliminarCliente(pCustomer, databaseConnection).ToString();
                if (result != null && result != "")
                {
                    APIResponse = Ok(result);
                }
                else
                {
                    APIResponse = Problem();
                }
            }
            catch (Exception)
            {
                APIResponse = BadRequest();
            }
            return APIResponse;
        }

        #endregion

        #region Metodos privados

        /// <summary>
        /// Obtiene la lista de los clientes
        /// </summary>
        /// <param name="databaseConnection"></param>
        /// <returns></returns>
        private List<CustomerEntitie> ObtieneClientesInfo(string databaseConnection)
        {
            try
            {
                CustomerBL customerBL = new CustomerBL();
                return customerBL.ObtenerClientes(databaseConnection).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la informacion de los clientes por nombre
        /// </summary>
        /// <param name="databaseConnection"></param>
        /// <param name="pNombre"></param>
        /// <returns></returns>
        private List<CustomerEntitie> ObtieneClientesXNombre(string databaseConnection, string pNombre)
        {
            List<CustomerEntitie> customerList = new List<CustomerEntitie>();
            try
            {
                CustomerBL customerBL = new CustomerBL();

                return customerBL.ObtenerClientesXNombre(databaseConnection, pNombre).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo privado que obtiene los clientes por cedula
        /// </summary>
        /// <param name="databaseConnection"></param>
        /// <param name="pCedula"></param>
        /// <returns></returns>
        private List<CustomerEntitie> ObtieneClientesXCedula(string databaseConnection, string pCedula)
        {
            List<CustomerEntitie> customerList = new List<CustomerEntitie>();
            try
            {
                CustomerBL customerBL = new CustomerBL();

                return customerBL.ObtenerClientesXCedula(databaseConnection, pCedula).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #endregion

        #region Articulos

        #region Metodos Publicos
        /// <summary>
        /// Metodo que envia todos los articulos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObtenerArticulos")]//Acceder a la ruta del metodo.
        public IEnumerable<ProductEntitie> ObtieneArticulos()
        {
            HttpResponseMessage APIResponse;
            List<ProductEntitie> listaArticulos = null;
            try
            {
                string DBConnection = _configuration.GetSection("ConnectionStrings").GetSection("conBD").Value; //Leer valor del settings.json
                return ObtieneArticulos(DBConnection).ToArray();
            }
            catch (Exception)
            {
                APIResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return listaArticulos;
            }
        }


        /// <summary>
        /// Obtiene los clientes x numero de cedula
        /// </summary>
        /// <param name="pArticulo"></param>
        /// <returns></returns>
        [HttpPost, HttpGet]
        [Route("ObtenerArticulosXId")]//Acceder a la ruta del metodo.
        public IEnumerable<ProductEntitie> ObtieneArticulosXId(ProductEntitie pArticulo)
        {
            HttpResponseMessage APIResponse;
            List<ProductEntitie> listaArticulos = null;
            try
            {
                string DBConnection = _configuration.GetSection("ConnectionStrings").GetSection("conBD").Value; //Leer valor del settings.json
                return ObtieneArticulosXId(pArticulo, DBConnection).ToArray();
            }
            catch (Exception)
            {
                APIResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return listaArticulos;
            }
        }

        /// <summary>
        /// Agrega los articulos
        /// </summary>
        /// <param name="pArticulo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AgregarArticulo")]//Acceder a la ruta del metodo.
        public ActionResult AgregarArticulo(ProductEntitie pArticulo)
        {
            string databaseConnection = _configuration.GetSection("ConnectionStrings").GetSection("conBD").Value; //Leer valor del settings.json
            ProductBL productBL = new ProductBL();
            string result = String.Empty;
            ActionResult APIResponse;
            try
            {
                result = productBL.AgregarArticulo(pArticulo, databaseConnection).ToString();
                APIResponse = Ok(result);
            }
            catch (Exception)
            {
                APIResponse = BadRequest();
            }
            return APIResponse;
        }

        /// <summary>
        /// Actualiza los articulos
        /// </summary>
        /// <param name="pArticulo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ActualizarArticulo")]//Acceder a la ruta del metodo.
        public ActionResult ActualizarArticulo(ProductEntitie pArticulo)
        {
            string databaseConnection = _configuration.GetSection("ConnectionStrings").GetSection("conBD").Value; //Leer valor del settings.json
            ProductBL productBL = new ProductBL();
            string result = String.Empty;
            ActionResult APIResponse;
            try
            {
                result = productBL.ActualizarArticulo(pArticulo, databaseConnection).ToString();
                APIResponse = Ok(result);
            }
            catch (Exception)
            {
                APIResponse = BadRequest();
            }
            return APIResponse;
        }

        /// <summary>
        /// Elimina los articulos
        /// </summary>
        /// <param name="pArticulo"></param>
        /// <returns></returns>
        [HttpPost, HttpDelete]
        [Route("EliminarArticulo")]//Acceder a la ruta del metodo.
        public ActionResult EliminarArticulo(ProductEntitie pArticulo)
        {
            string databaseConnection = _configuration.GetSection("ConnectionStrings").GetSection("conBD").Value; //Leer valor del settings.json
            ProductBL productBL = new ProductBL();
            string result = String.Empty;
            ActionResult APIResponse;
            try
            {
                result = productBL.EliminarArticulo(pArticulo, databaseConnection).ToString();
                APIResponse = Ok(result);
            }
            catch (Exception)
            {
                APIResponse = BadRequest();
            }
            return APIResponse;
        }


        /// <summary>
        /// Agrega la informacion de los clientes
        /// </summary>
        /// <param name="pFactura"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GenerarFactura")]//Acceder a la ruta del metodo.
        public ActionResult GenerarCompra(List<InvoiceCustomer> pFactura)
        {
            int resultado = 0;
            try
            {
                ProductBL productBL = new ProductBL();
                string databaseConnection = _configuration.GetSection("ConnectionStrings").GetSection("conBD").Value; //Leer valor del settings.json
                resultado = productBL.GenerarCompra(pFactura, databaseConnection);

                if (resultado != 0)
                {
                    return Ok(JsonConvert.SerializeObject(resultado));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #endregion

        #region Metodos Privados 
        /// <summary>
        /// Obtiene la lista de articulos
        /// </summary>
        /// <param name="databaseConnection"></param>
        /// <returns></returns>
        private List<ProductEntitie> ObtieneArticulos(string databaseConnection)
        {
            try
            {
                ProductBL productoBL = new ProductBL();
                return productoBL.ObtenerArticulo(databaseConnection).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene los articulos x id
        /// </summary>
        /// <param name="pArticulo"></param>
        /// <param name="databaseConnection"></param>
        /// <returns></returns>
        private List<ProductEntitie> ObtieneArticulosXId(ProductEntitie pArticulo, string databaseConnection)
        {
            try
            {
                ProductBL productoBL = new ProductBL();
                return productoBL.ObtenerArticuloXId(pArticulo.IdArticulo, databaseConnection).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion
    }
}
