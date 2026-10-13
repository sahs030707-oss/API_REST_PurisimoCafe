using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Capa_Entidades
{
    [Table("Ventas")]
    public class Ventas
    {
        [Key]
        public int VentaID { get; set; }

        public int? UsuarioID { get; set; }

        [Required]
        public DateTime FechaVenta { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        // Calculado por el SP — no viene de la tabla directamente
        [NotMapped]
        public string Cajero { get; set; } = string.Empty;

        // Suma de (CostoProduccion * Cantidad) de los detalles
        // Permite al frontend calcular: Ganancia = Total - CostoTotal
        [NotMapped]
        public decimal CostoTotal { get; set; }

        public List<DetalleVenta> Detalles { get; set; } = new();
    }
}
