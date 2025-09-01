using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace UniversidadDos.Models;

public partial class Estudiante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Correo { get; set; }

    [SwaggerSchema(Format = "date", Description = "Fecha de nacimineto en formato yyyy-MM-dd")]
    public DateOnly? FechaNacimiento { get; set; }

    public virtual ICollection<EstudianteMaterium> EstudianteMateria { get; set; } = new List<EstudianteMaterium>();
}

