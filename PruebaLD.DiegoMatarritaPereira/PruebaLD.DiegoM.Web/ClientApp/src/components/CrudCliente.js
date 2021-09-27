import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import swal from 'sweetalert';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';

function CrudCliente(props) {

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

    const mostrarAlertaInformativa = () => {
        swal({
            title: "Datos requeridos",
            text: "Por favor complete todos los datos",
            icon: "info",
            button: "Aceptar"
        })
    }

    const mostrarAlertaTieneFac = () => {
        swal({
            title: "Información",
            text: "Este cliente tiene facturas, no se puede eliminar",
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
    // #endregion

    // #region Manejo Modals

    // Estado para controlar abrir y cerrar modal
    const [modalInserta, setModalInsertar] = useState(false);
    const [modalActualiza, setModalActualizar] = useState(false);
    const [modalElimina, setModalEliminar] = useState(false);

    const abrirCerrarModalInsertar = () => {
        setModalInsertar(!modalInserta);
    }

    const abrirCerrarModalActualizar = () => {
        setModalActualizar(!modalActualiza);
    }

    const abrirCerrarModalEliminar = () => {
        setModalEliminar(!modalElimina);
    }

    // #endregion

    // #region Peticiones

    const [data, setdata] = useState([]);
    const [gestorSeleccionado, setGestorSeleccionado] = useState({
        cedulaCliente: '',
        nombreCliente: '',
        apellido: ''
    })

    const SeleccionarGestor = (datos, accion) => {
        setGestorSeleccionado(datos);
        (accion === "Editar") ?
            abrirCerrarModalActualizar() : abrirCerrarModalEliminar();
    }

    // Se realiza la peticion get para traer los clientes
    const peticionGet = async () => {
        await axios.get(baseUrl + "ObtenerClientes")
            .then(response => {
                setdata(response.data);
            }).catch(error => {
                console.log(error)
            })
    }

    // Se realiza peticion post para insercion 
    const peticionPostInsertarCliente = async () => {
        const config = { headers: { 'Content-Type': 'application/json' } };
        if (gestorSeleccionado.cedulaCliente === "" || gestorSeleccionado.nombreCliente === "" || gestorSeleccionado.Apellido === "") {
            mostrarAlertaInformativa();
        }
        else {
            await axios.post(baseUrl + "AgregarCliente", gestorSeleccionado)
                .then(response => {
                    setdata(data.concat(response.data));
                    mostrarAlertaExitosa();
                    abrirCerrarModalInsertar();
                    peticionGet();

                }).catch(error => {
                    mostrarAlertaError();
                    console.log(error)
                })
        }
    }

    const peticionPut = async () => {
        const config = { headers: { 'Content-Type': 'application/json' } };
        await axios.put(baseUrl + "ActualizarCliente", gestorSeleccionado)
            .then(response => {
                var respuesta = response.data;
                var dataAuxiliar = data;
                dataAuxiliar.map(datos => {
                    if (datos.cedulaCliente === gestorSeleccionado.cedulaCliente) {
                        datos.cedulaCliente = respuesta.cedulaCliente;
                        datos.nombreCliente = respuesta.nombreCliente;
                        datos.apellido = respuesta.apellido;
                    }
                })
                mostrarAlertaExitosa();
                abrirCerrarModalActualizar();
                peticionGet();
            }).catch(error => {
                mostrarAlertaError();
                console.log(error)
            })

    }

    const peticionDelete = async () => {
        const config = { headers: { 'Content-Type': 'application/json' } };
        await axios.post(baseUrl + "EliminarCliente", gestorSeleccionado)
            .then(response => {
                setdata(data.filter(datos => datos.cedulaCliente !== response.data));
                if (response.data === "No se puede eliminar el cliente ya que posee facturas") {
                    mostrarAlertaTieneFac();
                    abrirCerrarModalEliminar();
                }
                else {
                    abrirCerrarModalEliminar();
                    mostrarAlertaExitosa();
                    peticionGet();
                }
            }).catch(error => {
                mostrarAlertaError();
                console.log(error)
            })

    }
    // aqui se captura lo que el usuario escribe en los inputs 
    // se guarda en el estado en base al atribute name del modal
    const handleChange = e => {
        const { name, value } = e.target;
        setGestorSeleccionado({
            ...gestorSeleccionado,
            [name]: value
        });
    }

    useEffect(() => {
        peticionGet();
    }, [])

    // #endregion

    return (
        // Se pinta la tabla con los datos 
        <div className="container">
            <br></br>
            <button className="btn btn-info" onClick={() => abrirCerrarModalInsertar()}>Agregar Cliente</button>
            <br></br>
            <br></br>
            <br></br>
            <table className="table table-bordered">
                <thead>
                    <tr>
                        <th> Cedula Cliente</th>
                        <th> Nombre Cliente </th>
                        <th> Apellido Cliente </th>
                        <th> Acciones </th>
                    </tr>
                </thead>
                <tbody>
                    {data.map(datos => (
                        <tr key={datos.cedulaCliente}>
                            <td>{datos.cedulaCliente}</td>
                            <td>{datos.nombreCliente}</td>
                            <td>{datos.apellido}</td>
                            <td>
                                <button className="btn btn-primary" onClick={() => SeleccionarGestor(datos, "Editar")}>Editar</button> {"      "}
                                <button className="btn btn-danger" onClick={() => SeleccionarGestor(datos, "Eliminar")}>Eliminar</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            {/*  Modal agregar*/}
            {/*cedulaCliente: '',*/}
            {/*nombreCliente: '',*/}
            {/*apellido: '',*/}
            <Modal isOpen={modalInserta}>
                <ModalHeader> Insertar Datos del Cliente </ModalHeader>
                <ModalBody>
                    <div className="form-group">
                        <label>Cédula:</label>
                        <input type="text" className="form-control" name="cedulaCliente" onChange={handleChange} />
                        <br></br>
                        <label>Nombre:</label>
                        <input type="text" className="form-control" name="nombreCliente" onChange={handleChange} />
                        <br></br>
                        <label>Apellidos:</label>
                        <input type="text" className="form-control" name="apellido" onChange={handleChange} />
                        <br></br>
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button className="btn btn-danger" onClick={() => abrirCerrarModalInsertar()}>Cerrar</button> {"    "}
                    <button className="btn btn-primary" onClick={() => peticionPostInsertarCliente()}>Agregar</button>
                </ModalFooter>
            </Modal>
            {/*Modal Editar*/}
            <Modal isOpen={modalActualiza}>
                <ModalHeader> Actualizar Datos del Cliente </ModalHeader>
                <ModalBody>
                    <div className="form-group">
                        <label>Cédula:</label>
                        <input type="text" className="form-control" name="cedulaCliente" readOnly onChange={handleChange} value={gestorSeleccionado && gestorSeleccionado.cedulaCliente} />
                        <br></br>
                        <label>Nombre:</label>
                        <input type="text" className="form-control" name="nombreCliente" onChange={handleChange} value={gestorSeleccionado && gestorSeleccionado.nombreCliente} />
                        <br></br>
                        <label>Apellidos:</label>
                        <input type="text" className="form-control" name="apellido" onChange={handleChange} value={gestorSeleccionado && gestorSeleccionado.apellido} />
                        <br></br>
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button className="btn btn-danger" onClick={() => abrirCerrarModalActualizar()} >Cerrar</button> {"    "}
                    <button className="btn btn-primary" onClick={() => peticionPut()} >Editar</button>
                </ModalFooter>
            </Modal>
            {/*Modal Eliminar*/}
            <Modal isOpen={modalElimina}>
                <ModalBody>
                    Está seguro que desea eliminar el registro {gestorSeleccionado && gestorSeleccionado.cedulaCliente}?
                </ModalBody>
                <ModalFooter>
                    <button className="btn btn-danger" onClick={() => peticionDelete()}> Sí </button>
                    <button className="btn btn-secondary" onClick={() => abrirCerrarModalEliminar()}> No </button>
                </ModalFooter>
            </Modal>
        </div>
    );
}

export default CrudCliente;