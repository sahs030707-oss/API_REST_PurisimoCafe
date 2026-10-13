using System.ComponentModel.DataAnnotations;


namespace Capa_Entidades
{
    public class Proveedor
    {
        [Key]
        public int ProveedorID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage ="El nombre no puede contener mas de 100 caracteres.")]
        public string? Nombre { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        [StringLength(255)]
        public string? Direccion { get; set; }

        public bool Estado { get; set; } = true;
    }
}
