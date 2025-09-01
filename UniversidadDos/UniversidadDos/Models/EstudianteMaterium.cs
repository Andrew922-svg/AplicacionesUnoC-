using System;
using System.Collections.Generic;

namespace UniversidadDos.Models;

public partial class EstudianteMaterium
{
    public int Id { get; set; }

    public int? Nota1 { get; set; }

    public int? Nota2 { get; set; }

    public int? Nota3 { get; set; }

    public int EstuId { get; set; }

    public int MateId { get; set; }

    public virtual Estudiante Estu { get; set; } = null!;

    public virtual Materia Mate { get; set; } = null!;
}
