
--Eliminar categoria
CREATE PROCEDURE [dbo].[Sp_EliminarCategoria]
    @CategoriaID INT
AS
BEGIN
    UPDATE Categorias
    SET Estado = 0
    WHERE CategoriaID = @CategoriaID
END
