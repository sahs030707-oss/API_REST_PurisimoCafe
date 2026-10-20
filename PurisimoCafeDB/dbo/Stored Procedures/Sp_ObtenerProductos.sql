CREATE PROCEDURE [dbo].[Sp_ObtenerProductos]
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
    -- SIN filtro de estado: el frontend decide qué mostrar
END
