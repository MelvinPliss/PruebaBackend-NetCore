using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using PruebaBackendAPI.DLL.DTOs;
using PruebaBackendAPI.DLL.Models;

namespace PruebaBackendAPI.DLL.Repositories
{
    public class CalificacionRepository
    {
        private readonly AlumnosDbContext _context;

        public CalificacionRepository(AlumnosDbContext context)
        {
            _context = context;
        }

        public async Task<List<CalificacionDTO>> ConsultarCalificaciones(int idAlumno, int idGrado, int mes, int anio)
        {
            var lista = await _context.CalificacionDTOs
                .FromSqlRaw("CALL SP_CONSULTAR_CALIFICACIONES({0}, {1}, {2}, {3})",
                    idAlumno, idGrado, mes, anio)
                .ToListAsync();

            return lista;
        }

        public async Task<int> GuardarCalificacion(int alumnoId, int materiaId, decimal nota, int mes, int anio)
        {
            try
            {
                var resultado = await _context.CalificacionGuardarDTOs.FromSqlRaw("CALL SP_CAPTURAR_CALIFICACION({0},{1},{2},{3},{4})",
                    alumnoId, materiaId, mes, anio, nota).ToListAsync();

                return resultado.FirstOrDefault()?.Id ?? 0;
            }
            catch (Exception ex) {
                throw new Exception("Error al guardar calificacion", ex);
            }
        }
    }

}
