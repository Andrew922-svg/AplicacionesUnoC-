import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";

export default function ModalEditar(show) {

return (
    <>
        <Modal
        show = {show}
        onHide = {handleClose}
        backdrop = "static"
        keyboard = {false}>
            <Modal.Header closeButton>
                <Modal.Title /> hola
            </Modal.Header>
            <Modal.Body />
            <Modal.Footer>
                <Button variant="secondary" onClick={handleClose}>
                    Cerrar
                </Button>
                <Button variant="primary">
                    Guardar
                </Button>
            </Modal.Footer>
        </Modal>
    </>
);
}

