CREATE PROCEDURE [dbo].[Sp_ObtenerVentas]
AS
BEGIN
    SELECT
        v.VentaID,
        v.FechaVenta,
        ISNULL(u.Nombre, 'Sin asignar') AS Cajero,
        v.Total,
        -- CostoTotal: suma de (CostoProduccion * Cantidad) de los detalles
        -- Permite calcular Ganancia = Total - CostoTotal en el frontend
        ISNULL((
            SELECT SUM(p.CostoProduccion * dv.Cantidad)
            FROM DetalleVenta dv
            INNER JOIN Productos p ON dv.ProductoID = p.ProductoID
            WHERE dv.VentaID = v.VentaID
        ), 0) AS CostoTotal
    FROM Ventas v
    LEFT JOIN Usuarios u ON v.UsuarioID = u.UsuarioID
    ORDER BY v.FechaVenta DESC
END
