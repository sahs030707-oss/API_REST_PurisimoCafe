
--OBTENER POR ID
CREATE PROCEDURE [dbo].[Sp_ObtenerProductoPorId]
    @ProductoID INT
AS
BEGIN
    SELECT 
        p.ProductoID,
        p.CategoriaID,
        c.Nombre AS Categoria,
        p.Nombre,
        p.Descripcion,
        p.CostoProduccion,
        p.PrecioUnitario,
        p.Estado
    FROM Productos p
    INNER JOIN Categorias c
        ON p.CategoriaID = c.CategoriaID
    WHERE p.ProductoID = @ProductoID
END
