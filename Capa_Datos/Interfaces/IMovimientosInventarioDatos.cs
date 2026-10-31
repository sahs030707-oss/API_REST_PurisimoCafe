using Capa_Entidades;

namespace Capa_Datos
{
    // Interfaz añadida para permitir el mockeo de MovimientosInventarioDatos en pruebas unitarias.
    public interface IMovimientosInventarioDatos
    {
        void InsertarMovimiento(MovimientosInventario movimiento);
        IEnumerable<MovimientosInventario> ObtenerMovimientos();
    }
}
