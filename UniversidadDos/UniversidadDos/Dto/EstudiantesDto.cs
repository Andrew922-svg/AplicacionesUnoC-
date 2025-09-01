namespace UniversidadDos.Dto
{
    public class EstudiantesDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        

    }
}
