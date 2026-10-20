CREATE VIEW [dbo].[Vista_GananciasPorVenta] AS
SELECT
    v.VentaID,
    v.FechaVenta,
    ISNULL(u.Nombre, 'Sin asignar') AS Cajero,
    v.Total                          AS Ingreso,
    ISNULL(SUM(p.CostoProduccion * dv.Cantidad), 0) AS Costo,
    v.Total - ISNULL(SUM(p.CostoProduccion * dv.Cantidad), 0) AS Ganancia
FROM Ventas v
LEFT JOIN Usuarios u     ON v.UsuarioID  = u.UsuarioID
LEFT JOIN DetalleVenta dv ON v.VentaID   = dv.VentaID
LEFT JOIN Productos p     ON dv.ProductoID = p.ProductoID
GROUP BY
    v.VentaID, v.FechaVenta, u.Nombre, v.Total;

