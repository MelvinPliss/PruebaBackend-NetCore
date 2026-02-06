using System;
using System.Collections.Generic;

namespace PruebaBackendAPI.Models;

public partial class Materia
{
    public int IdMateria { get; set; }

    public string? Nombre { get; set; }

    public int? IdGrado { get; set; }

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    public virtual Grado? IdGradoNavigation { get; set; }
}
