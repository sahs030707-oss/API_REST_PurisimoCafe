CREATE VIEW [dbo].[Vista_StockActual] AS
SELECT
    p.ProductoID,
    p.Nombre                                            AS Producto,
    c.Nombre                                            AS Categoria,
    p.Estado,
    ISNULL(i.StockActual, 0)                           AS StockActual,
    p.PrecioUnitario,
    p.CostoProduccion,
    ISNULL(i.StockActual, 0) * p.PrecioUnitario        AS ValorInventarioVenta,
    ISNULL(i.StockActual, 0) * p.CostoProduccion       AS ValorInventarioCosto,
    CASE
        WHEN ISNULL(i.StockActual, 0) = 0  THEN 'Sin stock'
        WHEN ISNULL(i.StockActual, 0) <= 5 THEN 'Crítico'
        WHEN ISNULL(i.StockActual, 0) <= 15 THEN 'Bajo'
        ELSE 'Suficiente'
    END                                                AS NivelStock
FROM Productos p
LEFT JOIN Inventario i  ON p.ProductoID  = i.ProductoID
INNER JOIN Categorias c ON p.CategoriaID = c.CategoriaID
WHERE p.Estado = 1;

