using Capa_Entidades;

namespace Capa_Negocio
{
    public class CompraServicio
    {
        private readonly Capa_Datos.ICompraDatos _compraDatos;

        public CompraServicio(Capa_Datos.ICompraDatos compraDatos)
        {
            _compraDatos = compraDatos;
        }

        public void RegistrarCompra(Compra compra, List<DetalleCompra> detalles)
        {
            if (detalles == null || !detalles.Any())
                throw new Exception("La compra debe contener al menos un producto.");

            foreach (var det in detalles)
                det.Subtotal = det.PrecioUnitario * det.Cantidad;

            compra.Total = detalles.Sum(d => d.Subtotal);
            compra.FechaCompra = DateTime.Now;

            _compraDatos.RegistrarCompra(compra, detalles);
        }

        public List<Compra> ObtenerCompras() => _compraDatos.ObtenerCompras();

        public Compra? ObtenerCompraPorId(int id) => _compraDatos.ObtenerCompraPorId(id);
    }
}