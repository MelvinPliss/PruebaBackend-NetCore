using Microsoft.IdentityModel.Tokens;
using PruebaBackendAPI.DLL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PruebaBackendAPI.Models;
using System.Security.Cryptography;

namespace PruebaBackendAPI.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly AlumnosDbContext _context;
        private readonly IConfiguration _configuration;

        public AutorizacionService(AlumnosDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> DevolverToken(AutorizacionRequest autorizacion)
        {

            var usuario = _context.Usuarios.FirstOrDefault(x => x.NombreUsuario == autorizacion.NombreUsuario &&
                x.Clave == GetSha256(autorizacion.Clave)
            );

            if (usuario == null)
            {
                return "";
            }

            string tokenCreado = GenerarToken(usuario.IdUsuario.ToString(), usuario.Rol);

            return tokenCreado;
        }

        private string GenerarToken(string idUsuario, string rol)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));
            claims.AddClaim(new Claim(ClaimTypes.Role, rol));

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }

        // Función reutilizable para obtener SHA256
        private string GetSha256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            }
        }
    }
}
