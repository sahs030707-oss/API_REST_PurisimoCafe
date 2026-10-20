
-- Vistas 

CREATE VIEW [dbo].[Vista_VentaDetalladas] AS
SELECT
    v.VentaID,
    v.FechaVenta,
    u.Nombre AS Usuario,
    v.Total,
    dv.ProductoID,
    p.Nombre AS Producto,
    dv.Cantidad,
    dv.SubTotal
FROM Ventas v
INNER JOIN Usuarios u
    ON v.UsuarioID = u.UsuarioID
INNER JOIN DetalleVenta dv
    ON v.VentaID = dv.VentaID
INNER JOIN Productos p
    ON dv.ProductoID = p.ProductoID;
