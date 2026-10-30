using Capa_Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Capa_Datos
{
    // Implementa IMovimientosInventarioDatos para permitir inyección y mocking en pruebas unitarias
    public class MovimientosInventarioDatos : IMovimientosInventarioDatos
    {
        private readonly string _connectionString;

        public MovimientosInventarioDatos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void InsertarMovimiento(MovimientosInventario movimiento)
        {
            using var con = new SqlConnection(_connectionString);

            con.Open();

            using var cmd = new SqlCommand("Sp_InsertarMovimientoInventario", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ProductoID", movimiento.ProductoID);
            cmd.Parameters.AddWithValue("@Cantidad", movimiento.Cantidad);
            cmd.Parameters.AddWithValue("@Motivo", movimiento.Motivo);

            cmd.ExecuteNonQuery();
        }

        public IEnumerable<MovimientosInventario> ObtenerMovimientos()
        {
            var lista = new List<MovimientosInventario>();
            using var con = new SqlConnection(_connectionString);
            con.Open();

            using var cmd = new SqlCommand("Sp_ObtenerMovimientos", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new MovimientosInventario
                {
                    MovimientoID = reader.GetInt32(0),
                    ProductoID = reader.GetInt32(1),
                    NombreProducto = reader.GetString(2),
                    Cantidad = reader.GetInt32(3),
                    Motivo = reader.GetString(4),
                    Fecha = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5)
                });
            }
            return lista;
        }
    }
}