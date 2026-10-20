
--Procedimiento almacenado De Categoria

--obtener Categoria
CREATE PROCEDURE [dbo].[Sp_ObtenerCategorias]
AS
BEGIN
    SELECT 
        CategoriaID,
        Nombre,
        Descripcion,
        Estado
    FROM Categorias
    WHERE Estado = 1
END
