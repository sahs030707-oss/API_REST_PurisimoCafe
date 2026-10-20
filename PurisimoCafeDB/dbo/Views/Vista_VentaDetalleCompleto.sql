CREATE VIEW [dbo].[Vista_VentaDetalleCompleto] AS
SELECT
    v.VentaID,
    CAST(v.FechaVenta AS DATE)                              AS Fecha,
    v.FechaVenta                                            AS FechaHora,
    ISNULL(u.Nombre, 'Sin asignar')                        AS Cajero,
    dv.ProductoID,
    ISNULL(dv.NombreProducto, p.Nombre)                    AS Producto,
    c.Nombre                                                AS Categoria,
    dv.Cantidad,
    dv.PrecioUnitario,
    p.CostoProduccion,
    dv.SubTotal                                             AS Ingreso,
    p.CostoProduccion * dv.Cantidad                        AS Costo,
    dv.SubTotal - (p.CostoProduccion * dv.Cantidad)        AS Ganancia,
    CASE WHEN dv.SubTotal = 0 THEN 0
         ELSE CAST(
             (dv.SubTotal - p.CostoProduccion * dv.Cantidad)
             * 100.0 / dv.SubTotal AS DECIMAL(5,2))
    END                                                     AS MargenPct
FROM Ventas v
INNER JOIN DetalleVenta dv ON v.VentaID     = dv.VentaID
INNER JOIN Productos p     ON dv.ProductoID = p.ProductoID
INNER JOIN Categorias c    ON p.CategoriaID = c.CategoriaID
LEFT  JOIN Usuarios u      ON v.UsuarioID   = u.UsuarioID;

