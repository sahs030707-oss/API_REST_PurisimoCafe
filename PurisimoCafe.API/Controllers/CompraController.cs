using Capa_Entidades;
using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PurisimoCafe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CompraController : ControllerBase
    {
        private readonly CompraServicio _compraServicio;

        public CompraController(CompraServicio compraServicio)
        {
            _compraServicio = compraServicio;
        }

        /// <summary>
        /// Registra Una Nueva Compra Con Sus Detalles.
        /// </summary>
        [HttpPost("Registrar")]
        public IActionResult RegistrarCompra([FromBody] CompraRequest request)
        {
            try
            {
                if (request?.Detalles == null || request.Detalles.Count == 0)
                    return BadRequest("Debe enviar al menos un producto en los detalles.");

                // Obtener UsuarioID del token JWT
                var usuarioIdClaim = User.FindFirst("usuarioId")?.Value;
                int? usuarioId = int.TryParse(usuarioIdClaim, out int uid) ? uid : null;

                var compra = new Compra
                {
                    ProveedorID = request.ProveedorID,
                    UsuarioID = usuarioId,
                    MetodoPago = request.MetodoPago
                };
                _compraServicio.RegistrarCompra(compra, request.Detalles);

                return Ok(new { Mensaje = "Compra registrada correctamente", CompraID = compra.CompraID });
            }
            catch (Exception ex)
            {
                var msg = ex.Message + (ex.InnerException != null ? " | " + ex.InnerException.Message : "");
                return StatusCode(500, $"Error al registrar la compra: {msg}");
            }
        }

        /// <summary>
        /// Muestra Todas Las Compras Registradas En El Sistema.
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerCompras()
        {
            var compras = _compraServicio.ObtenerCompras();
            return Ok(compras);
        }

        /// <summary>
        /// Muestra Una Compra Por Su ID Con Detalles Completos.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult ObtenerCompraPorId(int id)
        {
            if (id <= 0) return BadRequest("ID inválido.");
            var compra = _compraServicio.ObtenerCompraPorId(id);
            if (compra == null) return NotFound($"No se encontró la compra con ID {id}.");
            return Ok(compra);
        }
    }

    public class CompraRequest
    {
        public int? ProveedorID { get; set; }
        public string? MetodoPago { get; set; }
        public List<DetalleCompra> Detalles { get; set; } = new();
    }
}