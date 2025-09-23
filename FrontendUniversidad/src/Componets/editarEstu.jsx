import { useState } from "react";
import { appsettings } from "../settings/appsettings";
import { Form, FormGroup, Label, Input, Button } from "reactstrap";

export default function EditarEstudiante({estudiante, onSave, onCancel}) {

    const [estudainteSeleccionado, setEstudianteSeleccionado] = useState ({

        id: estudiante.id ,
        nombre: estudiante ? estudiante.nombre: "",
        apellido: estudiante ? estudiante.apellido: "",
        correo: estudiante ? estudiante.correo: "",
        fechaNaciemineto: estudiante ? estudiante.fechaNaciemineto: "",
    });

    const handleChange = (e) => {
        setEstudianteSeleccionado ({
            ...estudainteSeleccionado,
            [e.target.name]: e.target.value
        });
    }

    const handleSubmit = () => {
        onSave(formData);
    }

    return(
        <Form>
            <FormGroup>
                <Label for="nombre">
                    Nombre
                </Label>
                <Input type="text" name="nombre" id="nombre" 
                value={estudainteSeleccionado.nombre}
                onChange={handleChange}
                required />
            </FormGroup>
            <FormGroup>
                <Label for="apellido">
                    apellido
                </Label>
                <Input type="text" name="apellido" id="apellido"
                value={estudainteSeleccionado.apellido}
                onChange={handleChange}
                required />
            </FormGroup>
            <FormGroup>
                <Label for="correo">
                    Correo
                </Label>
                <Input type="email" name="correo" id="correo"
                value={estudainteSeleccionado.correo}
                onChange={handleChange}
                required/>
            </FormGroup>
            <FormGroup>
                <Label for="fechaNacimiento">
                    Fecah de Nacimiento.
                </Label>
                <Input type="date" name="fechaNacimiento" id="fechaNacimiento"
                value={estudainteSeleccionado.fechaNaciemineto}
                onChange={handleChange}
                required/>
            </FormGroup>
            <Button type="submit" color="success" outline className="me-3">Guardar</Button>
            <Button type="button" color="info" outline onClick={onCancel}>Cancelar</Button>
        </Form>
    )
}