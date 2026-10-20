CREATE VIEW [dbo].[Vista_VentasPorCategoria] AS
SELECT
    c.CategoriaID,
    c.Nombre                                                AS Categoria,
    COUNT(DISTINCT dv.VentaID)                             AS NumVentas,
    SUM(dv.Cantidad)                                       AS UnidadesVendidas,
    SUM(dv.SubTotal)                                       AS IngresoTotal,
    SUM(p.CostoProduccion * dv.Cantidad)                  AS CostoTotal,
    SUM(dv.SubTotal) - SUM(p.CostoProduccion * dv.Cantidad) AS GananciaNeta
FROM DetalleVenta dv
INNER JOIN Productos p  ON dv.ProductoID  = p.ProductoID
INNER JOIN Categorias c ON p.CategoriaID  = c.CategoriaID
GROUP BY c.CategoriaID, c.Nombre;

