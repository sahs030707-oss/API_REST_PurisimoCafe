
--Obtener por ID
CREATE PROCEDURE [dbo].[Sp_ObtenerCategoriaPorId]
    @CategoriaID INT
AS
BEGIN
    SELECT 
        CategoriaID,
        Nombre,
        Descripcion,
        Estado
    FROM Categorias
    WHERE CategoriaID = @CategoriaID
END
