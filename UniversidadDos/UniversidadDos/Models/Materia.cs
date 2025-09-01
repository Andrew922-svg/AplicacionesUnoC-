using System;
using System.Collections.Generic;

namespace UniversidadDos.Models;

public partial class Materia
{
    public int Id { get; set; }

    public string? NombreMateria { get; set; }

    public virtual ICollection<EstudianteMaterium> EstudianteMateria { get; set; } = new List<EstudianteMaterium>();
}
