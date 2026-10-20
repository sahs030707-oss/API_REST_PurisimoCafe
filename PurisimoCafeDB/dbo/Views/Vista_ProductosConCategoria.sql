

CREATE VIEW [dbo].[Vista_ProductosConCategoria] AS
SELECT 
    p.ProductoID,
    p.Nombre AS NombreProducto,
    p.Descripcion,
    c.Nombre AS Categoria,
    c.Descripcion AS DescripcionCategoria
FROM Productos p
INNER JOIN Categorias c ON p.CategoriaID = c.CategoriaID;
