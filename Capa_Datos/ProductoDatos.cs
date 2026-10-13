using System.Data;
using Capa_Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Capa_Datos
{
    public class ProductoDatos : IProductoDatos
    {
        private readonly string _connectionString;

        public ProductoDatos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Producto> GetProductos()
        {
            var productos = new List<Producto>();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using SqlCommand cmd = new("Sp_ObtenerProductos", connection) { CommandType = CommandType.StoredProcedure };
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(new Producto
                {
                    ProductoID = Convert.ToInt32(reader["ProductoID"]),
                    CategoriaID = Convert.ToInt32(reader["CategoriaID"]),
                    Nombre = reader["Nombre"].ToString(),
                    Descripcion = reader["Descripcion"].ToString(),
                    Estado = Convert.ToBoolean(reader["Estado"]),
                    CostoProduccion = Convert.ToDecimal(reader["CostoProduccion"]),
                    PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"])
                });
            }
            return productos;
        }

        public Producto? GetProductoById(int id)
        {
            Producto? producto = null;
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using SqlCommand cmd = new("Sp_ObtenerProductoPorId", connection) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@ProductoID", id);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                producto = new Producto
                {
                    ProductoID = Convert.ToInt32(reader["ProductoID"]),
                    CategoriaID = Convert.ToInt32(reader["CategoriaID"]),
                    Nombre = reader["Nombre"].ToString(),
                    Descripcion = reader["Descripcion"].ToString(),
                    Estado = Convert.ToBoolean(reader["Estado"]),
                    CostoProduccion = Convert.ToDecimal(reader["CostoProduccion"]),
                    PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"])
                };
            }
            return producto;
        }

        public void Insertar(Producto producto)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using SqlCommand cmd = new("Sp_InsertarProducto", connection) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@CategoriaID", producto.CategoriaID);
            cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? string.Empty);
            cmd.Parameters.AddWithValue("@CostoProduccion", producto.CostoProduccion);
            cmd.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioUnitario);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Producto producto)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using SqlCommand cmd = new("Sp_ActualizarProducto", connection) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@ProductoID", producto.ProductoID);
            cmd.Parameters.AddWithValue("@CategoriaID", producto.CategoriaID);
            cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? string.Empty);
            cmd.Parameters.AddWithValue("@CostoProduccion", producto.CostoProduccion);
            cmd.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioUnitario);
            cmd.Parameters.AddWithValue("@Estado", producto.Estado); // ← línea que faltaba
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using SqlCommand cmd = new("Sp_EliminarProducto", connection) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@ProductoID", id);
            cmd.ExecuteNonQuery();
        }
    }
}