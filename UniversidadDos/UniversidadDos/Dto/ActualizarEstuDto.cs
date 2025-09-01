namespace UniversidadDos.Dto
{
    public class ActualizarEstuDto
    {
        public string Nombre { get; set; }  
        public string Apellido {  get; set; }
        public string Correo { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
    }
}
