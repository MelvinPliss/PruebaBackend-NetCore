using System;
using System.Collections.Generic;

namespace PruebaBackendAPI.DLL.Models;

public partial class Alumno
{
    public int IdAlumno { get; set; }

    public string Matricula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public DateOnly FechaDeNacimiento { get; set; }

    public string Genero { get; set; } = null!;

    public string? Estatus { get; set; }

    public int? IdGrado { get; set; }

    public virtual ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();

    public virtual Grado? IdGradoNavigation { get; set; }
}
