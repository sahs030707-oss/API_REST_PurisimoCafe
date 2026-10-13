using Capa_Entidades;

namespace Capa_Datos
{
    public interface ICompraDatos
    {
        void RegistrarCompra(Compra compra, List<DetalleCompra> detalles);
        List<Compra> ObtenerCompras();
        Compra? ObtenerCompraPorId(int id);
    }
}