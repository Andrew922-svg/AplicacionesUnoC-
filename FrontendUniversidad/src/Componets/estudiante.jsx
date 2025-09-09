import {ChangeEvent, useEffect, useState} from "react";
import { useNavigate } from "react-router-dom";
import Swal from "sweetalert2";
import { Container, Row, Col, Form, FormGroup, Label, Input, Button, Modal } from "reactstrap";
import { appsettings } from "../settings/appsettings";


export function Listar(){
    const [show, setShow] = useState(false);
    const [EstSelect, setEstuSelect] = useState(null);

    const handleclose = () => setShow(false);
    const handleShow = (estu)=>{
        setEstuSelect(estu);
        setShow(true);
    };

    const [estudiante, setEstudiante] = useState([]);
    <h1>Listar</h1>
    useEffect(() => {
        fetch(appsettings.apiUrl + "/api/Usuario/listar/estudiante")
        .then((Response) => Response.json())
        .then((data) => setEstudiante(data))
        .catch((error) => console.error("Error al mostrar los estudiantes", error));
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
                        <Button className="EditarEstu" style={{backgroundColor: "red", borderColor: "blue"}} onClick={() =>(handleShow(estu))}>
                            Editar
                        </Button>
                    </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            
        </Container>
    );
}
