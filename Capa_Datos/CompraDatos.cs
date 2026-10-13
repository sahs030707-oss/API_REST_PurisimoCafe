using Capa_Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Capa_Datos
{
    public class CompraDatos : ICompraDatos
    {
        private readonly string _connectionString;

        public CompraDatos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public void RegistrarCompra(Compra compra, List<DetalleCompra> detalles)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();
            using var transaccion = conexion.BeginTransaction();

            try
            {
                using var cmdCompra = new SqlCommand("Sp_InsertarCompra", conexion, transaccion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmdCompra.Parameters.AddWithValue("@ProveedorID", (object?)compra.ProveedorID ?? DBNull.Value);
                cmdCompra.Parameters.AddWithValue("@UsuarioID", (object?)compra.UsuarioID ?? DBNull.Value);
                cmdCompra.Parameters.AddWithValue("@MetodoPago", (object?)compra.MetodoPago ?? DBNull.Value);
                cmdCompra.Parameters.AddWithValue("@Total", compra.Total);
                cmdCompra.Parameters.AddWithValue("@FechaCompra", compra.FechaCompra);

                var paramCompraID = new SqlParameter("@CompraID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmdCompra.Parameters.Add(paramCompraID);
                cmdCompra.ExecuteNonQuery();

                int compraId = (int)paramCompraID.Value;
                compra.CompraID = compraId;

                foreach (var det in detalles)
                {
                    using var cmdDetalle = new SqlCommand("Sp_InsertarDetalleCompra", conexion, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmdDetalle.Parameters.AddWithValue("@CompraID", compraId);
                    cmdDetalle.Parameters.AddWithValue("@ProductoID", det.ProductoID);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", det.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", det.PrecioUnitario);
                    cmdDetalle.Parameters.AddWithValue("@Subtotal", det.Subtotal);

                    cmdDetalle.ExecuteNonQuery();
                }
                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public List<Compra> ObtenerCompras()
        {
            var lista = new List<Compra>();
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();
            using var cmd = new SqlCommand("Sp_ObtenerCompras", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Compra
                {
                    CompraID = Convert.ToInt32(reader["CompraID"]),
                    ProveedorID = reader["ProveedorID"] as int?,
                    NombreProveedor = reader["NombreProveedor"]?.ToString() ?? string.Empty,
                    UsuarioID = reader["UsuarioID"] as int?,
                    NombreUsuario = reader["NombreUsuario"]?.ToString() ?? string.Empty,
                    MetodoPago = reader["MetodoPago"]?.ToString(),
                    Total = Convert.ToDecimal(reader["Total"]),
                    FechaCompra = Convert.ToDateTime(reader["FechaCompra"])
                });
            }
            return lista;
        }

        public Compra? ObtenerCompraPorId(int id)
        {
            Compra? compra = null;
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();
            using var cmd = new SqlCommand("Sp_ObtenerCompraPorId", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@CompraID", id);
            using var reader = cmd.ExecuteReader();

            // Encabezado
            if (reader.Read())
            {
                compra = new Compra
                {
                    CompraID = Convert.ToInt32(reader["CompraID"]),
                    ProveedorID = reader["ProveedorID"] as int?,
                    NombreProveedor = reader["NombreProveedor"]?.ToString() ?? string.Empty,
                    UsuarioID = reader["UsuarioID"] as int?,
                    NombreUsuario = reader["NombreUsuario"]?.ToString() ?? string.Empty,
                    MetodoPago = reader["MetodoPago"]?.ToString(),
                    Total = Convert.ToDecimal(reader["Total"]),
                    FechaCompra = Convert.ToDateTime(reader["FechaCompra"]),
                    Detalles = new List<DetalleCompra>()
                };
            }

            if (compra != null && reader.NextResult())
            {
                while (reader.Read())
                {
                    compra.Detalles.Add(new DetalleCompra
                    {
                        DetalleCompraID = Convert.ToInt32(reader["DetalleCompraID"]),
                        CompraID = Convert.ToInt32(reader["CompraID"]),
                        ProductoID = Convert.ToInt32(reader["ProductoID"]),
                        Nombre = reader["NombreProducto"]?.ToString() ?? string.Empty,
                        PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        Subtotal = Convert.ToDecimal(reader["Subtotal"])
                    });
                }

            }

            return compra;
        }
    }
}