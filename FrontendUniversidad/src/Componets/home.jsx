import { Container, Button } from "reactstrap";
import { useNavigate } from "react-router-dom";

export default function PagHome() {

    const navigation = useNavigate();

    return (
        <Container className="text-center mt-5">
            <h1> Bienvenido al sistemas de gestion universitaria.</h1> 
            <table className="d-flex flex-column gap-3 align-items-center mt-4">
                <h5>Hola</h5>
                        <thead className="Tabla_primaria text-center aling-middle ">
                        <tr>
                        <thead className="px-4 py-5"> Listar Estudiantes
                            <Button color="success" className="mt-3" outline onClick={() => navigation("/estudiante")}>
                                Listar.
                            </Button>
                        </thead>
                        <thead className="px-4 py-5"> Listar Estudiantes
                            <Button color="success" outline onClick={() => navigation("/estudiante")}>
                                <i className="bi bi-person me-2 "> Listar. </i>
                            </Button>
                        </thead>
                        </tr>
                    </thead>
            </table>
        </Container>
    )
}