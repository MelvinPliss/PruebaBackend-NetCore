using System;
using System.Collections.Generic;

namespace PruebaBackendAPI.Models;

public partial class Grado
{
    public int IdGrado { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    public virtual ICollection<Materia> Materia { get; set; } = new List<Materia>();
}
