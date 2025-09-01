using System.ComponentModel.DataAnnotations;

namespace UniversidadDos.Dto
{
    public class AgregarNotas
    {
        [Required]
        public string NombreEstudiante { get; set; }
        
        [Required]
        public string NombreMateria { get; set; }

        [Required]
        public int CalificacionUno { get; set; }

        [Required]

        public int CalificacionDos {  get; set; }

        [Required]

        public int CalificacionTres { get; set; }

    }
}
