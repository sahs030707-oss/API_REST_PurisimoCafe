using Capa_Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Capa_Datos
{
    public class VentaDatos : IVentaDatos
    {
        private readonly string _connectionString;

        public VentaDatos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public void RegistrarVenta(Ventas venta, List<DetalleVenta> detalles)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();
            using var transaccion = conexion.BeginTransaction();

            try
            {
                // ── 1. Insertar encabezado de venta ──
                using var cmdVenta = new SqlCommand("Sp_InsertarVenta", conexion, transaccion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmdVenta.Parameters.AddWithValue("@FechaVenta", venta.FechaVenta);
                cmdVenta.Parameters.AddWithValue("@Total", venta.Total);
                cmdVenta.Parameters.AddWithValue("@UsuarioID", (object?)venta.UsuarioID ?? DBNull.Value);

                var paramVentaID = new SqlParameter("@VentaID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmdVenta.Parameters.Add(paramVentaID);
                cmdVenta.ExecuteNonQuery();

                int ventaId = (int)paramVentaID.Value;
                venta.VentaID = ventaId;   // devolver el ID generado

                // ── 2. Insertar cada detalle ──
                foreach (var det in detalles)
                {
                    using var cmdDetalle = new SqlCommand("Sp_InsertarDetalleVenta", conexion, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmdDetalle.Parameters.AddWithValue("@VentaID", ventaId);
                    cmdDetalle.Parameters.AddWithValue("@ProductoID", det.ProductoID);
                    // NombreProducto: guardar snapshot del nombre en el momento de la venta
                    cmdDetalle.Parameters.AddWithValue("@NombreProducto", (object?)det.Nombre ?? DBNull.Value);
                    cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", det.PrecioUnitario);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", det.Cantidad);
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

        public List<Ventas> ObtenerVentas()
        {
            var lista = new List<Ventas>();
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();
            using var cmd = new SqlCommand("Sp_ObtenerVentas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Ventas
                {
                    VentaID = Convert.ToInt32(reader["VentaID"]),
                    FechaVenta = Convert.ToDateTime(reader["FechaVenta"]),
                    Total = Convert.ToDecimal(reader["Total"]),
                    Cajero = reader["Cajero"].ToString() ?? string.Empty,
                    CostoTotal = Convert.ToDecimal(reader["CostoTotal"])
                });
            }
            return lista;
        }

        public Ventas? ObtenerVentaPorId(int id)
        {
            Ventas? venta = null;
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();
            using var cmd = new SqlCommand("Sp_ObtenerVentaPorId", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@VentaID", id);
            using var reader = cmd.ExecuteReader();

            // Encabezado
            if (reader.Read())
            {
                venta = new Ventas
                {
                    VentaID = Convert.ToInt32(reader["VentaID"]),
                    FechaVenta = Convert.ToDateTime(reader["FechaVenta"]),
                    Total = Convert.ToDecimal(reader["Total"]),
                    Cajero = reader["Cajero"].ToString() ?? string.Empty,
                    CostoTotal = Convert.ToDecimal(reader["CostoTotal"]),
                    Detalles = new List<DetalleVenta>()
                };
            }

            // Detalles
            if (venta != null && reader.NextResult())
            {
                while (reader.Read())
                {
                    venta.Detalles.Add(new DetalleVenta
                    {
                        DetalleVentaID = Convert.ToInt32(reader["DetalleVentaID"]),
                        VentaID = Convert.ToInt32(reader["VentaID"]),
                        ProductoID = Convert.ToInt32(reader["ProductoID"]),
                        Nombre = reader["NombreProducto"].ToString() ?? string.Empty,
                        PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        Subtotal = Convert.ToDecimal(reader["SubTotal"]),
                        CostoProduccion = Convert.ToDecimal(reader["CostoProduccion"])
                    });
                }
            }

            return venta;
        }
    }
}
