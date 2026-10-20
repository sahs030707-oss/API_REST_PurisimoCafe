
--Insertar Categoria
CREATE PROCEDURE [dbo].[Sp_InsertarCategoria]
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(255)
AS
BEGIN
    INSERT INTO Categorias(Nombre, Descripcion)
    VALUES (@Nombre, @Descripcion);
END
