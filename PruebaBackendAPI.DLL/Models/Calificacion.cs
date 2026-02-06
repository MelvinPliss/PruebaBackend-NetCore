using System;
using System.Collections.Generic;

namespace PruebaBackendAPI.DLL.Models;

public partial class Calificacion
{
    public int IdCalificacion { get; set; }

    public int? IdAlumno { get; set; }

    public int? IdMateria { get; set; }

    public decimal Nota { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? IdGrado { get; set; }

    public virtual Alumno? IdAlumnoNavigation { get; set; }

    public virtual Grado? IdGradoNavigation { get; set; }

    public virtual Materia? IdMateriaNavigation { get; set; }
}
