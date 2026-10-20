

CREATE VIEW [dbo].[Vista_ProductosMasVendidos] AS
SELECT
    p.ProductoID,
    p.Nombre AS Producto,
    SUM(dv.Cantidad) AS TotalVendido
FROM DetalleVenta dv
INNER JOIN Productos p
    ON dv.ProductoID = p.ProductoID
GROUP BY
    p.ProductoID,
    p.Nombre;
