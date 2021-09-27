import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import swal from 'sweetalert';
import NumberFormat from 'react-number-format';

function CrearFactura(props) {
    // #region Constantes
    const baseUrl = "http://localhost:24251/api/Customer/";
    // #endregion

    // #region Alertas
    const mostrarAlertaExitosa = () => {
        swal({
            title: "Accion realizada con éxito",
            text: "Se realizó correctamente la acción",
            icon: "success",
            button: "Aceptar"
        })
    }

    const mostrarAlertaFEExitosa = () => {
        swal({
            title: "Factura Creada",
            text: "Su factura se creó correctamente",
            icon: "success",
            button: "Aceptar"
        }).then((result) => {
            window.location.reload();
        });
    }

    const mostrarAlertaInformativa = () => {
        swal({
            title: "Datos requeridos",
            text: "Por favor complete todos los datos",
            icon: "info",
            button: "Aceptar"
        })
    }

    const mostrarAlertaError = () => {
        swal({
            title: "Se produjo un error",
            text: "Por favor intentelo mas tarde",
            icon: "error",
            button: "Aceptar"
        })
    }
    const mostrarAlertaErrorFE = () => {
        swal({
            title: "Se produjo un error al crear la factura",
            text: "Por favor intentelo mas tarde",
            icon: "error",
            button: "Aceptar"
        })
    }

    
    // #endregion

    // #region Peticiones


    const [emps, setEmps] = useState([]);
    //sección para ver u ocultar la tabla
    const [showResults, setShowResults] = useState(false);
    const [datoTotal, setDatoTotal] = useState("0");
    const [datoCantidad, setDatoCantidad] = useState("0")
    const [datoPrecio, setDatoPrecio] = useState("");
    const [datoIdArticulo, setDatoArticulo] = useState("");
    const [datoNombre, setDatoNombre] = useState("");
    const [datoNombreCliente, setDatoNombreCliente] = useState("");
    const [datoApellidoCliente, setDatoApellidoCliente] = useState("");
    const [datoCedula, setdatoCedula] = useState("");
    const [inputValue, setInputValue] = useState("");
    const [inputValueCed, setInputValueCed] = useState("");
    const [articuloSeleccionadoInput, setArticuloSeleccionadoInput] = useState({
        idArticulo: '',
        NombreArticulo: '',
        CantidadArticulo: 0,
        PrecioArticulo: 0,
        descripcionArticulo: ""
    })
    const [clienteSeleccionadoInput, setClienteSeleccionadoInput] = useState({
        cedulaCliente: '',
        NombreCliente: '',
        apellidoCliente: ""
    })

    // Se realiza la peticion get para traer los articulos
    const peticionGet = async (valor) => {
        articuloSeleccionadoInput.idArticulo = parseInt(valor);
        const config = { headers: { 'Content-Type': 'application/json' } };
        await axios.post(baseUrl + "ObtenerArticulosXId", articuloSeleccionadoInput, config)
            .then(response => {
                setDatoNombre(response.data[0].nombreArticulo);
                setDatoPrecio(response.data[0].precioArticulo);
                setDatoArticulo(response.data[0].idArticulo);
            }).catch(error => {
                console.log(error)
            })
    }

    // Se realiza la peticion get para traer los clientes
    const peticionGetClientes = async (valorCed) => {
        clienteSeleccionadoInput.cedulaCliente = valorCed;
        const config = { headers: { 'Content-Type': 'application/json' } };
        await axios.post(baseUrl + "ObtenerClientesXCedula", clienteSeleccionadoInput, config)
            .then(response => {
                if (response.data.length === 1) {
                    setDatoNombreCliente(response.data[0].nombreCliente);
                    setDatoApellidoCliente(response.data[0].apellido);
                }
            }).catch(error => {
                console.log(error)
                mostrarAlertaError();
            })
    }

    // Obtiene el valor del input de id articulo
    const handleChange = e => {
        const { name, value } = e.target;
        setArticuloSeleccionadoInput({
            ...articuloSeleccionadoInput,
            [name]: value
        });
        setInputValue(e.target.value);
        peticionGet(value);
    }

    // Obtiene el valor de input cedula
    const handleChangeCedula = e => {
        const { name, value } = e.target;
        setClienteSeleccionadoInput({
            ...clienteSeleccionadoInput,
            [name]: value
        });
        setInputValueCed(e.target.value);
        setdatoCedula(e.target.value);
        peticionGetClientes(value);
    }

    // Obtiene valor de input cantidad
    const handleChangeCantidad = e => {
        const { name, value } = e.target;
        setArticuloSeleccionadoInput({
            ...articuloSeleccionadoInput,
            [name]: value
        });

        setDatoCantidad(e.target.value);
    }

    //Aqui se realiza la peticion para pintar la tabla de productos a comprar
    const peticionCargar = () => {

        var totalLinea = parseFloat(datoPrecio * datoCantidad);
        var totalFactura = parseFloat(datoTotal) + totalLinea;
        setDatoTotal(totalFactura.toFixed(2).toLocaleString());
        var nombreCliente = datoNombreCliente + " " + datoApellidoCliente;
        var cedulaCliente = datoCedula
        let newEmp = {
            idArticulo: datoIdArticulo,
            Nombre: datoNombre,
            Precio: parseFloat(datoPrecio).toFixed(2).toLocaleString(),
            cantidadProducto: parseInt(datoCantidad),
            TotalLinea: parseFloat(totalLinea).toFixed(2).toLocaleString(),
            ClienteFactura: nombreCliente,
            CedulaCliente: cedulaCliente
        }
        setEmps([...emps, newEmp])
        setShowResults(true);//visualizar tabla
    }

    const realizarFactura = async () => {
        //enviar datos de tabla emps
        const config = { headers: { 'Content-Type': 'application/json' } };

        await axios.post(baseUrl + "GenerarFactura", emps, config)
            .then(response => {
                if (response.data != -1 && response.data != 0) {
                    mostrarAlertaFEExitosa();
                    window.location.reload(false);
                }
                else {
                    mostrarAlertaErrorFE();
                }
            }).catch(error => {
                console.log();
                mostrarAlertaError();
            })
    }
    // #endregion

    // #region TablaFactura
    const Results = () => (
        <div>
            <table className="display display table responsive nowra">
                <thead>
                    <tr>
                        <th>Cédula del cliente</th>
                        <th>Nombre del cliente</th>
                        <th>Código del artículo</th>
                        <th>Nombre del artículo</th>
                        <th>Precio</th>
                        <th>Cantidad</th>
                        <th>Total</th>
                    </tr>
                </thead>
                <tbody>
                    {emps.map((articulo, index) =>
                    (
                        <tr key={index}>
                            <td>{articulo.CedulaCliente}</td>
                            <td>{articulo.ClienteFactura}</td>
                            <td>{articulo.idArticulo}</td>
                            <td>{articulo.Nombre}</td>
                            <td>{articulo.Precio}</td>
                            <td>{articulo.cantidadProducto}</td>
                            <td>{articulo.TotalLinea}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <div className="" id="divTotalMonto">
                <div className="d-flex flex-row-reverse">
                    <div classname="p-2">total: <input type="text" placeholder="0" id="txttotal" name="total" classname="form-control" readonly value={datoTotal} /></div>
                </div>
            </div>
            <div className="col-12 pt-1 text-center">
                <button className="btn btn-primary btn-md" onClick={realizarFactura}> Realizar Factura </button>
            </div>
        </div>
    )
    // #endregion

    return (
        // Cuadros de texto 
        <div className="container">
            <div className="container">
                <div className="row" id="">
                    <div className="col-md-4">
                        <div className="form-group">
                            <p>Cédula:</p>
                            <input type="text" placeholder="0" name="Cedula" className="form-control" onChange={handleChangeCedula} value={inputValueCed} maxLength="15" minLength="1" />
                        </div>
                    </div>
                    <div className="col-md-4">
                        <div className="form-group">
                            <p >Nombre :</p>
                            <input type="text" placeholder="Nombre cliente" name="nombreCliente" className="form-control" maxLength="100" readOnly value={datoNombreCliente + " " + datoApellidoCliente} />
                        </div>
                    </div>
                </div>
                <div className="row" id="">
                    <div className="col-md-3">
                        <div className="form-group">
                            <p>Código Artículo:</p>
                            <input type="text" placeholder="0" name="Codigo" className="form-control" onChange={handleChange} value={inputValue} />
                        </div>
                    </div>
                    <div className="col-md-3">
                        <div className="form-group">
                            <p >Nombre :</p>
                            <input type="text" placeholder="Nombre Articulo" name="NombreArticulo" className="form-control" maxLength="100" readOnly value={datoNombre} />
                        </div>
                    </div>
                    <div className="col-md-3">
                        <div className="form-group">
                            <p >Cantidad :</p>
                            <NumberFormat decimalSeparator={false} className="form-control" inputMode="numeric" allowNegative={false} name="Precio" onChange={handleChangeCantidad} />
                            <input type="hidden" placeholder="0" id="txtPrecio" name="PrecioArticulo" value={datoPrecio} readOnly />
                        </div>
                    </div>
                    <div className="col-md-3">
                        <div className="form-group">
                            <p>Acción</p>
                            <button className="btn btn-primary btn-md" id="btnAgregar" onClick={peticionCargar}> Agregar Artículo</button>
                        </div>
                    </div>
                </div>
            </div><br />
            <div className="ContenidoPrincipal">
                {showResults ? <Results /> : null}
            </div>
        </div>
    );
}

export default CrearFactura;