import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import swal from 'sweetalert';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';

function CrudArticulos(props) {

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
    // Estado para controlar abrir y cerrar modals

    const [modalInserta, setModalInsertar] = useState(false);
    const abrirCerrarModalInsertar = () => {
        setModalInsertar(!modalInserta);
    }

    const [modalActualiza, setModalActualizar] = useState(false);
    const abrirCerrarModalActualizar = () => {
        setModalActualizar(!modalActualiza);
    }

    const [modalElimina, setModalEliminar] = useState(false);
    const abrirCerrarModalEliminar = () => {
        setModalEliminar(!modalElimina);
    }

    // #endregion

    // #region Peticiones

    const [data, setdata] = useState([]);
    const [articuloSeleccionado, setArticuloSelec] = useState({
        nombreArticulo: '',
        precioArticulo: '',
        descripcionArticulo: ''
    })

    const SeleccionarAccion = (datos, accion) => {
        setArticuloSelec(datos);
        (accion === "Editar") ?
            abrirCerrarModalActualizar() : abrirCerrarModalEliminar();
    }

    // Se realiza la peticion get para traer los clientes
    const peticionGet = async () => {
        await axios.get(baseUrl + "ObtenerArticulos")
            .then(response => {
                setdata(response.data);
            }).catch(error => {
                console.log(error)
            })
    }

    const peticionPost = async () => {
        const config = { headers: { 'Content-Type': 'application/json' } };
        if (articuloSeleccionado.nombreArticulo === "" || articuloSeleccionado.precioArticulo === "" || articuloSeleccionado.descripcionArticulo === "") {
            mostrarAlertaInformativa();
        }
        else {
            articuloSeleccionado.precioArticulo = parseFloat(articuloSeleccionado.precioArticulo);
            await axios.post(baseUrl + "AgregarArticulo", articuloSeleccionado)
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
        articuloSeleccionado.precioArticulo = parseFloat(articuloSeleccionado.precioArticulo);
        articuloSeleccionado.idArticulo = parseInt(articuloSeleccionado.idArticulo);
        await axios.put(baseUrl + "ActualizarArticulo", articuloSeleccionado)
            .then(response => {
                var respuesta = response.data;
                var dataAuxiliar = data;
                dataAuxiliar.map(datos => {
                    if (datos.idArticulo === articuloSeleccionado.idArticulo) {
                        datos.idArticulo = respuesta.idArticulo;
                        datos.nombreArticulo = respuesta.nombreArticulo;
                        datos.precioArticulo = respuesta.precioArticulo;
                        datos.descripcionArticulo = respuesta.descripcionArticulo;
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
        await axios.post(baseUrl + "EliminarArticulo", articuloSeleccionado)
            .then(response => {
                setdata(data.filter(datos => datos.idArticulo !== response.data));
                abrirCerrarModalEliminar();
                mostrarAlertaExitosa();
                peticionGet();
            }).catch(error => {
                mostrarAlertaError();
                console.log(error)
            })

    }

    // aqui se captura lo que el usuario escribe en los inputs 
    // se guarda en el estado en base al atribute name del modal
    const handleChange = e => {
        const { name, value } = e.target;
        setArticuloSelec({
            ...articuloSeleccionado,
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
            <button className="btn btn-dark" onClick={() => abrirCerrarModalInsertar()}>Agregar artículo</button>
            <br></br>
            <br></br>
            <br></br>
            <br></br>
            <table className="table table-hover table-bordered" >
                <thead>
                    <tr>
                        <th> Código Artículo</th>
                        <th> Nombre Artículo </th>
                        <th> Descripción Artículo </th>
                        <th> Precio Artículo </th>
                        <th> Acciones </th>
                    </tr>
                </thead>
                <tbody>
                    {data.map(datosArticulo => (
                        <tr key={datosArticulo.idArticulo}>
                            <td>{datosArticulo.idArticulo}</td>
                            <td>{datosArticulo.nombreArticulo}</td>
                            <td>{datosArticulo.descripcionArticulo}</td>
                            <td>{datosArticulo.precioArticulo}</td>
                            <td>
                                <button className="btn btn-primary" onClick={() => SeleccionarAccion(datosArticulo, "Editar")}>Editar</button> {"                "}
                                <button className="btn btn-danger" onClick={() => SeleccionarAccion(datosArticulo, "Eliminar")}>Eliminar</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            {/*ModalInsertar*/}
            <Modal isOpen={modalInserta}>
                <ModalHeader> Agregar Artículos </ModalHeader>
                <ModalBody>
                    <div className="form-group">
                        <label>Nombre Artículo:</label>
                        <input type="text" className="form-control" name="nombreArticulo" onChange={handleChange} />
                        <br></br>
                        <label>Precio:</label>
                        <input type="text" className="form-control" name="precioArticulo" onChange={handleChange} maxLength="100" />
                        <br></br>
                        <label>Descripción:</label>
                        <textarea type="text" className="form-control" name="descripcionArticulo" onChange={handleChange} maxLength="100" rows="4" cols="50"> </textarea><br></br>
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button className="btn btn-danger" onClick={() => abrirCerrarModalInsertar()}>Cerrar</button> {"    "}
                    <button className="btn btn-primary" onClick={() => peticionPost()}>Agregar</button>
                </ModalFooter>
            </Modal>
            {/*Modal Actualizar*/}
            <Modal isOpen={modalActualiza}>
                <ModalHeader> Actualizar datos del artículo </ModalHeader>
                <ModalBody>
                    <div className="form-group">
                        <label>Código Artículo:</label>
                        <input type="text" className="form-control" name="idArticulo" onChange={handleChange} readOnly value={articuloSeleccionado && articuloSeleccionado.idArticulo} />
                        <br></br>
                        <label>Nombre Artículo:</label>
                        <input type="text" className="form-control" name="nombreArticulo" onChange={handleChange} value={articuloSeleccionado && articuloSeleccionado.nombreArticulo} />
                        <br></br>
                        <label>Precio:</label>
                        <input type="text" className="form-control" name="precioArticulo" onChange={handleChange} maxLength="100" value={articuloSeleccionado && articuloSeleccionado.precioArticulo} />
                        <br></br>
                        <label>Descripción:</label>
                        <textarea type="text" className="form-control" name="descripcionArticulo" onChange={handleChange} maxLength="100" rows="4" cols="50" value={articuloSeleccionado && articuloSeleccionado.descripcionArticulo}> </textarea><br></br>
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button className="btn btn-danger" onClick={() => abrirCerrarModalActualizar()}>Cerrar</button> {"    "}
                    <button className="btn btn-primary" onClick={() => peticionPut()}>Editar</button>
                </ModalFooter>
            </Modal>
            {/*Modal Eliminar*/}
            <Modal isOpen={modalElimina}>
                <ModalBody>
                    Está seguro que desea eliminar el registro {articuloSeleccionado && articuloSeleccionado.idArticulo}?
                </ModalBody>
                <ModalFooter>
                    <button className="btn btn-danger" onClick={() => peticionDelete()}> Sí </button>
                    <button className="btn btn-secondary" onClick={() => abrirCerrarModalEliminar()}> No </button>
                </ModalFooter>
            </Modal>
        </div>
    );
}

export default CrudArticulos;