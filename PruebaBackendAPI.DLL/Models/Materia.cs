using System;
using System.Collections.Generic;

namespace PruebaBackendAPI.DLL.Models;

public partial class Materia
{
    public int IdMateria { get; set; }

    public string? Nombre { get; set; }

    public int? IdGrado { get; set; }

    public virtual ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();

    public virtual Grado? IdGradoNavigation { get; set; }
}
