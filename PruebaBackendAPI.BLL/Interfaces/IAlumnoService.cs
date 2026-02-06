using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaBackendAPI.DLL.DTOs;

namespace PruebaBackendAPI.BLL.Interfaces
{
    public interface IAlumnoService
    {
        Task<List<CalificacionDTO>> ObtenerCalificaciones(int idAlumno, int idGrado, int mes, int anio);
        Task<int> GuardarCalificacion(int idAlumno, int idMateria, decimal nota, int mes, int anio);
    }
}
