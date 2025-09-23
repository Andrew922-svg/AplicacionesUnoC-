    import {ChangeEvent, useEffect, useState} from "react";
    import { Container, Row, Col, Form, FormGroup, Label, Input, Button} from "reactstrap";
    import { appsettings } from "../settings/appsettings";
    import Modal from "react-bootstrap/Modal";
    import { data } from "react-router-dom";
    import ModalEditar from "./ModalEn";
    import EditarEstudiante from "./editarEstu";


    export default function EstudianteListar(){
        const [show, setShow] = useState(false);
        const [EstSelect, setEstuSelect] = useState(null);
        const [estudiante, setEstudiante] = useState([]);
        const [mostrarShow, MostrarShow] = useState(false);
        
        const handleGuardar = (estudianteActualizado) => {
            fetch(appsettings.apiUrl + "api/Usuario/actualizar/estudiante/"+ estudianteActualizado.id,{
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(estudianteActualizado)
            })
            .then((res) =>{
                if (!res.ok) {
                    throw new Error ("Error al actualizar el estudiante");
                }
                return res.json();
            })
            .then((data) =>{
            console.log ("Estudiante actualizado con exito", data);
            setEstudiante((prev) => prev.map((e) => (e.id == data.id ? data : e)));
            MostrarShow(false);
            setEstuSelect(null);
            })
            .catch ((error) =>
            console.log("Error al actualizar el estudiante"));
        };


        const handleUpdate = async (id) => {
        try {
            const response = await fetch(`https://localhost:44359/api/Usuario/actualizar/estudiante/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                nombre: nombre,
                apellido: apellido,
                correo: correo,
                telefono: telefono
            })
            });

            if (response.ok) {
            const data = await response.json();
            console.log("Estudiante actualizado:", data);
            } else {
            console.error("Error al actualizar");
            }
        } catch (error) {
            console.error("Error en la petición:", error);
        }
        };

        const handleDelete = (id) =>{
            if (!window.confirm("¿Esta seguro de eliminar este usuario?")) return;
            fetch(appsettings.apiUrl + "api/Usuario/eliminar/estudiante/" + id, {
                method: "DELETE",
            })
            .then((res) => {
                if (!res.ok){
                    throw new Error("Error al eliminar el estudiante");
                }
                return res.text();
            })
            .then(() => {
                setEstudiante((prev) => prev.filter((e) => e.id !== id));
            })
            .catch((error) => console.log("Error al eliminar", error));
        };

        const handleCancelar = () => {
            MostrarShow (false);
            setEstuSelect(null);
        };

        const modalopen = () => MostrarShow (true);
        const modalClose = () => MostrarShow (false);

        const handleEdit = (estu) => {
            setEstuSelect (estu);
            MostrarShow (true);
        };

        useEffect (() =>{
            fetch(appsettings.apiUrl + "api/Usuario/listar/estudiante")
            .then((Response) => Response.json())
            .then((data) => setEstudiante (data))
            .catch((error) => console.log ("Error al mostrar los estudiantes", error));
        }, []);

        return(
            
            < Container>
                <h1 className="text-center color-red">Listar estudiantes</h1>
                <table>
                    <thead className="Tabla_primaria text-center aling-middle ">
                        <tr>
                        <th className="px-4 py-5"> Nombre</th>
                        <th className="px-4 py-5"> Apellido</th>
                        <th className="px-4 py5"> Correo</th>
                        <th className="px-4 py5">Fecha de nacimiento</th>
                        </tr>
                    </thead>
                    <tbody>
                        {estudiante.map((estu, index) => (
                            <tr key = {index}>
                                <td className="px-4 py5">{estu.nombre}</td>
                                <td className="px-4 py5">{estu.apellido}</td>
                                <td className="px-4 py5">{estu.correo}</td>
                                <td className="px-4 py5">{new Date(estu.fechaNacimiento).toLocaleDateString()}</td>
                            <td>
                            <Button className="me-2"  color="primary" outline onClick={() =>(handleEdit(estu))}>
                                Editar
                            </Button>
                        </td>
                        <td>
                            <Button className="me-2" color="danger" outline onClick={() => handleDelete(estu.id)}>
                                Eliminar
                            </Button> 
                        </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                <Modal 
                show={mostrarShow}
                onHide={modalClose}
                backdrop="static"
                keyboard={false}
                >
                    <Modal.Header closeButton>
                        <Modal.Title>
                            Editar estudiante.
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        {
                            EstSelect ? (
                                <EditarEstudiante
                                estudiante = {EstSelect}
                                onSave = {handleUpdate}
                                onCancel = {handleCancelar}
                                />
                            ):(
                                <p> No hay estudiante SELECCIONADO. </p>
                            )
                        }
                    </Modal.Body>
                </Modal>
            </Container>
        );
    }
