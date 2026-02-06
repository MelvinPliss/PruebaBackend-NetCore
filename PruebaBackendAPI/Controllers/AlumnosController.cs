using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaBackendAPI.BLL.Interfaces;
using PruebaBackendAPI.DLL.DTOs;
using PruebaBackendAPI.Models;

namespace PruebaBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;

        public AlumnosController(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

        [Authorize]
        [HttpPost]
        [Route("{alumnoId}/calificaciones")]
        public async Task<IActionResult> GuardarCalificacion(int alumnoId, [FromBody] AlumnoRequest request)
        {
            var resultado = new ApiResponse<int>();
            try
            {
                resultado.Status = true;
                resultado.Value = await _alumnoService.GuardarCalificacion(alumnoId, request.MateriaId,
                request.Nota, request.Mes, request.Anio); ;
            }
            catch (Exception e)
            {
                resultado.Status = false;
                resultado.Msg = e.Message;

                return BadRequest(resultado);
            }
            
            return Ok(resultado); 
        }

        [HttpGet]
        [Route("{alumnoId}/calificaciones")]
        public async Task<IActionResult> ConsultarCalificaciones(int alumnoId, int grado, int mes, int anio)
        {
            var resultado = new ApiResponse<List<CalificacionDTO>>();

            try
            {                
                resultado.Status = true;
                resultado.Value = await _alumnoService.ObtenerCalificaciones(alumnoId, grado, mes, anio); ;
            }
            catch (Exception e) { 
            
            }
            return Ok(resultado); 
        }
    }
}
