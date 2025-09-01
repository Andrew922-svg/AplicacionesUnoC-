using System.Threading.Tasks.Dataflow;

namespace UniversidadDos.Dto
{
    public class AgregarEstuDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public DateOnly? Fecha { get; set; }
    }
}
