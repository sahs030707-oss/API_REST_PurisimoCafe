using Capa_Entidades;
using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PurisimoCafe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorServicio _proveedorServicio;
        public ProveedorController(ProveedorServicio proveedorServicio)
        {
            _proveedorServicio = proveedorServicio;
        }

        /// <summary>
        /// Muestra Todos Los Proveedores Registrados En El Sistema.
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var lista = _proveedorServicio.GetProveedores();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener proveedores: {ex.Message}");
            }
        }

        /// <summary>
        /// Muestra Un Proveedor Registrado Por Su ID.
        /// </summary>
        /// <param name="id">ID del proveedor</param>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var proveedor = _proveedorServicio.GetProveedorById(id);
                if (proveedor == null) return NotFound($"No se encontró el proveedor con ID {id}");
                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener proveedor: {ex.Message}");
            }
        }

        /// <summary>
        /// Permite Insertar Un Nuevo Proveedor Al Sistema.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] Proveedor proveedor)
        {
            try
            {
                if (proveedor == null) return BadRequest("Datos inválidos");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _proveedorServicio.AddProveedor(proveedor);
                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar proveedor: {ex.Message}");
            }
        }

        /// <summary>
        /// Permite Actualizar Los Datos De Un Proveedor.
        /// </summary>
        /// <param name="id">ID del proveedor</param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Proveedor proveedor)
        {
            try
            {
                if (proveedor == null || proveedor.ProveedorID != id)
                    return BadRequest("Datos incorrectos");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _proveedorServicio.UpdateProveedor(proveedor);
                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar proveedor: {ex.Message}");
            }
        }

        /// <summary>
        /// Permite Eliminar (Desactivar) Un Proveedor A través De Su ID.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _proveedorServicio.DeleteProveedor(id);
                return Ok("Proveedor desactivado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al desactivar proveedor: {ex.Message}");
            }
        }
    }
}