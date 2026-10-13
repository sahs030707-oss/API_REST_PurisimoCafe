using Capa_Entidades;

namespace Capa_Datos
{
    public interface IProveedorDatos
    {
        IEnumerable<Proveedor> GetProveedores();
        Proveedor? GetProveedorById(int id);
        void Insertar(Proveedor proveedor);
        void Actualizar(Proveedor proveedor);
        void Eliminar(int id);
    }
}