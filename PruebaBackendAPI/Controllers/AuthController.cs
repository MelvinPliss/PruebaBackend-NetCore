using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaBackendAPI.Models;
using PruebaBackendAPI.Services;

namespace PruebaBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAutorizacionService _autorizacionService;

        public AuthController(IAutorizacionService autorizacionService)
        {
            _autorizacionService = autorizacionService;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Autenticar([FromBody] AutorizacionRequest autorizacion)
        {
            var resultado_autorizacion = await _autorizacionService.DevolverToken(autorizacion);
            if (resultado_autorizacion == null)
                return Unauthorized();

            return Ok(resultado_autorizacion);

        }

    }
}
