using Capa_Entidades;
using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PurisimoCafe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VentaController : ControllerBase
    {
        private readonly VentaServicio _ventaServicio;

        public VentaController(VentaServicio ventaServicio)
        {
            _ventaServicio = ventaServicio;
        }

        /// <summary>
        /// Registra Una Nueva Venta Con Sus Detalles.
        /// </summary>
        [HttpPost("Registrar")]
        public IActionResult RegistrarVenta([FromBody] VentaRequest request)
        {
            try
            {
                if (request?.Detalles == null || request.Detalles.Count == 0)
                    return BadRequest("Debe enviar al menos un producto en los detalles.");

                // Obtener UsuarioID del token JWT
                var usuarioIdClaim = User.FindFirst("usuarioId")?.Value;
                int? usuarioId = int.TryParse(usuarioIdClaim, out int uid) ? uid : null;

                var venta = new Ventas { UsuarioID = usuarioId };
                _ventaServicio.RegistrarVenta(venta, request.Detalles);

                return Ok(new { Mensaje = "Venta registrada correctamente", VentaID = venta.VentaID });
            }
            catch (Exception ex)
            {
                var msg = ex.Message + (ex.InnerException != null ? " | " + ex.InnerException.Message : "");
                if (msg.Contains("Stock insuficiente"))
                    return BadRequest("No hay stock suficiente para uno o más productos.");

                return StatusCode(500, $"Error al registrar la venta: {msg}");
            }
        }

        /// <summary>
        /// Muestra Todas Las Ventas Registradas En El Sistema.
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerVentas()
        {
            var ventas = _ventaServicio.ObtenerVentas();
            return Ok(ventas);
        }

        /// <summary>
        /// Muestra Una Venta Por Su ID Con Detalles Completos.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult ObtenerVentaPorId(int id)
        {
            if (id <= 0) return BadRequest("ID inválido.");
            var venta = _ventaServicio.ObtenerVentaPorId(id);
            if (venta == null) return NotFound($"No se encontró la venta con ID {id}.");
            return Ok(venta);
        }
    }

    // ── DTO de entrada ──
    public class VentaRequest
    {
        public List<DetalleVenta> Detalles { get; set; } = new();
    }
}
