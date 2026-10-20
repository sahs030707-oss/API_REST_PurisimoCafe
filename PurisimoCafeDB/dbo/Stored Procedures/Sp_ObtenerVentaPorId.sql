CREATE PROCEDURE [dbo].[Sp_ObtenerVentaPorId]
    @VentaID INT
AS
BEGIN
    -- Encabezado de la venta
    SELECT
        v.VentaID,
        v.FechaVenta,
        ISNULL(u.Nombre, 'Sin asignar') AS Cajero,
        v.Total,
        ISNULL((
            SELECT SUM(p.CostoProduccion * dv2.Cantidad)
            FROM DetalleVenta dv2
            INNER JOIN Productos p ON dv2.ProductoID = p.ProductoID
            WHERE dv2.VentaID = v.VentaID
        ), 0) AS CostoTotal
    FROM Ventas v
    LEFT JOIN Usuarios u ON v.UsuarioID = u.UsuarioID
    WHERE v.VentaID = @VentaID;

    -- Detalles con nombre del producto
    SELECT
        dv.DetalleVentaID,
        dv.VentaID,
        dv.ProductoID,
        -- Usar el nombre guardado en el momento de la venta;
        -- si es NULL (ventas viejas) usar el nombre actual del producto
        ISNULL(dv.NombreProducto, p.Nombre) AS NombreProducto,
        dv.PrecioUnitario,
        dv.Cantidad,
        dv.SubTotal,
        p.CostoProduccion
    FROM DetalleVenta dv
    INNER JOIN Productos p ON dv.ProductoID = p.ProductoID
    WHERE dv.VentaID = @VentaID;
END
