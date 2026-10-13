using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Capa_Entidades
{
    public class DetalleVenta
    {
        [Key]
        public int DetalleVentaID { get; set; }

        [JsonIgnore]
        public int VentaID { get; set; }

        public int ProductoID { get; set; }

        // Nombre del producto en el momento de la venta (snapshot)
        public string? Nombre { get; set; }

        public decimal PrecioUnitario { get; set; }

        public int Cantidad { get; set; }

        public decimal Subtotal { get; set; }

        // Viene del JOIN con Productos al leer — para calcular ganancia por ítem
        [NotMapped]
        public decimal CostoProduccion { get; set; }

        // Calculado: ganancia de este ítem = (Precio - Costo) * Cantidad
        [NotMapped]
        [JsonIgnore]
        public decimal GananciaItem => (PrecioUnitario - CostoProduccion) * Cantidad;
    }
}
