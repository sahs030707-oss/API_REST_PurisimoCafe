CREATE PROCEDURE [dbo].[Sp_ObtenerInventario]
AS
BEGIN
    SELECT
        i.InventarioID,
        p.ProductoID,
        p.Nombre        AS NombreProducto,
        c.Nombre        AS Categoria,
        p.PrecioUnitario,
        p.CostoProduccion,
        p.Estado,
        i.StockActual,
        i.Fecha
    FROM Inventario i
    INNER JOIN Productos p
        ON i.ProductoID = p.ProductoID
    INNER JOIN Categorias c
        ON p.CategoriaID = c.CategoriaID
    WHERE
        -- Activos: siempre mostrar (aunque stock sea 0)
        p.Estado = 1
        OR
        -- Inactivos: solo si aún tienen stock (desaparecen solos al llegar a 0)
        (p.Estado = 0 AND i.StockActual > 0)
END

