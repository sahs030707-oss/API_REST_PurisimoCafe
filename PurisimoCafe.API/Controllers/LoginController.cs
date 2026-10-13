using Capa_Entidades;
using Capa_Negocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PurisimoCafe.API.Controllers
{
    /// <summary>
    /// Inicio De Sesión Y Generación De Tokens JWT
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly PC_DBContext _context;
        private readonly IConfiguration _config;
        private readonly UsuarioServicio _usuarioServicio;

        public LoginController(PC_DBContext context, IConfiguration config, UsuarioServicio usuarioServicio)
        {
            _context = context;
            _config = config;
            _usuarioServicio = usuarioServicio;
        }

        /// <summary>
        /// Permite A Un Usuario Iniciar Sesión Y Obtener Un Token JWT.
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 1. Buscar usuario activo por correo
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == request.Correo && u.Estado == true);

            if (usuario == null)
                return Unauthorized("Usuario no encontrado o inactivo.");

            // 2. Verificar contraseña con Argon2id
            //    Formato almacenado: "base64salt.base64hash"
            if (!VerificarClave(request.Clave, usuario.Contraseña))
                return Unauthorized("Credenciales inválidas.");

            // 3. Generar Token JWT
            var jwtSettings = _config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Correo),
                new Claim("usuarioId", usuario.UsuarioID.ToString()),
                new Claim("rol",       usuario.RolID.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Usuario = usuario.Nombre,
                Correo = usuario.Correo,
                RolId = usuario.RolID,
                UsuarioId = usuario.UsuarioID
            });
        }

        // ── Verifica la clave ingresada contra el hash Argon2id almacenado ──
        private bool VerificarClave(string claveIngresada, string hashAlmacenado)
        {
            try
            {
                var partes = hashAlmacenado.Split('.');
                if (partes.Length != 2) return false;

                var salt = Convert.FromBase64String(partes[0]);
                var hashEsperado = partes[1];
                var hashCalculado = _usuarioServicio.HashearClave(claveIngresada, salt);

                // hashCalculado tiene el mismo formato "base64salt.base64hash"
                var hashCalculadoParte = hashCalculado.Split('.')[1];
                return hashCalculadoParte == hashEsperado;
            }
            catch
            {
                return false;
            }
        }
    }
}
