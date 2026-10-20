

CREATE VIEW [dbo].[Vista_InventarioCompleto] AS
SELECT 
    i.InventarioID,
    p.ProductoID,
    p.Nombre AS NombreProducto,
    c.Nombre AS Categoria,
    i.StockActual,
    i.Fecha AS FechaUltimoMovimiento
FROM Inventario i
INNER JOIN Productos p ON i.ProductoID = p.ProductoID
INNER JOIN Categorias c ON p.CategoriaID = c.CategoriaID;
