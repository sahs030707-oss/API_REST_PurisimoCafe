using Capa_Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Capa_Datos
{
    public class ProveedorDatos : IProveedorDatos
    {
        private readonly string _connectionString;

        public ProveedorDatos(IConfiguration configuration) 
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Proveedor> GetProveedores()
        {
            var proveedores = new List<Proveedor>();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using SqlCommand cmd = new("Sp_ObtenerProveedores", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                proveedores.Add(new Proveedor
                {
                    ProveedorID = Convert.ToInt32(reader["ProveedorID"]),
                    Nombre = reader["Nombre"].ToString(),
                    Telefono = reader["Telefono"].ToString(),
                    Direccion = reader["Direccion"].ToString(),
                    Estado = Convert.ToBoolean(reader["Estado"])
                });
            }
            return proveedores;
        }

        public Proveedor? GetProveedorById(int id)
        {
            Proveedor? proveedor = null;
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using SqlCommand cmd = new("Sp_ObtenerProveedorPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@ProveedorID", id);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                proveedor = new Proveedor
                {
                    ProveedorID = Convert.ToInt32(reader["ProveedorID"]),
                    Nombre = reader["Nombre"].ToString(),
                    Telefono = reader["Telefono"].ToString(),
                    Direccion = reader["Direccion"].ToString(),
                    Estado = Convert.ToBoolean(reader["Estado"])
                };
            }
            return proveedor;
        }

        public void Insertar (Proveedor proveedor)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using SqlCommand cmd = new("Sp_InsertarProveedor", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
            cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono ?? string.Empty);
            cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion ?? string.Empty);

            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Proveedor proveedor)
        {
            using SqlConnection connection = new (_connectionString);
            connection.Open();
            using SqlCommand cmd = new("Sp_ActualizarProveedor", connection)
            {
                CommandType= CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@ProveedorID", proveedor.ProveedorID);
            cmd.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
            cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono ?? string.Empty);
            cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion ?? string.Empty);
            cmd.Parameters.AddWithValue("@Estado", proveedor.Estado);

            cmd.ExecuteNonQuery();
        }

        public void Eliminar (int id)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using SqlCommand cmd = new("Sp_EliminarProveedor", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@ProveedorID", id);
            cmd.ExecuteNonQuery ();
        }
    }
}