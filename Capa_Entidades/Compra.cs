using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capa_Entidades
{
    [Table("Compras")]
    public class Compra
    {
        [Key]
        public int CompraID { get; set; }

        public int? ProveedorID { get; set; }

        public int? UsuarioID { get; set; }

        [StringLength(50)]
        public string? MetodoPago { get; set; }

        [Required]
        [Column(TypeName = "decimal (10,2)")]
        public decimal Total { get; set; }

        [Required]
        public DateTime FechaCompra {  get; set; }

        [NotMapped]
        public string NombreProveedor {  get; set; } = string.Empty;

        [NotMapped]
        public string NombreUsuario { get; set; } = string.Empty;

        public List<DetalleCompra> Detalles { get; set; } = new();


    }
}
