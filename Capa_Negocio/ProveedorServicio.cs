using Capa_Entidades;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Capa_Negocio
{
    public class ProveedorServicio
    {
        private readonly Capa_Datos.IProveedorDatos _proveedorDatos;

        public ProveedorServicio(Capa_Datos.IProveedorDatos proveedorDatos)
        {
            _proveedorDatos = proveedorDatos ?? throw new ArgumentNullException(nameof(proveedorDatos));
        }

        public IEnumerable<Proveedor> GetProveedores()
        {
            return _proveedorDatos.GetProveedores();
        }

        public Proveedor? GetProveedorById(int id)
        {
            return _proveedorDatos.GetProveedorById(id);
        }

        public void AddProveedor(Proveedor proveedor)
        {
            if (proveedor == null) throw new ArgumentNullException(nameof(proveedor));
            _proveedorDatos.Insertar(proveedor);
        }

        public void UpdateProveedor(Proveedor proveedor)
        {
            if (proveedor == null) throw new ArgumentNullException(nameof(proveedor));
            _proveedorDatos.Actualizar(proveedor);
        }

        public void DeleteProveedor(int id)
        {
            _proveedorDatos.Eliminar(id);
        }
    }
}
