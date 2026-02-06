using PruebaBackendAPI.BLL.Interfaces;
using PruebaBackendAPI.DLL.DTOs;
using PruebaBackendAPI.DLL.Repositories;

namespace PruebaBackendAPI.BLL.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly CalificacionRepository _calificacionRepository;
        public AlumnoService(CalificacionRepository calificacionRepository) { 
            _calificacionRepository = calificacionRepository;
        }

        public async Task<int> GuardarCalificacion(int idAlumno, int idMateria, decimal nota, int mes, int anio)
        {
            try
            {
                if (nota < 0 && nota > 10)
                    throw new TaskCanceledException("El valor de la nota debe estar entre el 0 y 10");

                var idGenerado = await _calificacionRepository.GuardarCalificacion(idAlumno, idMateria, nota, mes, anio);
                return idGenerado;
            }
            catch
            {
                throw;
            }            
        }

        public async Task<List<CalificacionDTO>> ObtenerCalificaciones(int idAlumno, int idGrado, int mes, int anio)
        {
            return await _calificacionRepository.ConsultarCalificaciones(idAlumno, idGrado, mes, anio);
        }
    }
}
