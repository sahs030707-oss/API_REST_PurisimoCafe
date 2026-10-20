
--ACTUALIZAR
CREATE PROCEDURE [dbo].[Sp_ActualizarProducto]
    @ProductoID INT,
    @CategoriaID INT,
    @Nombre NVARCHAR(60),
    @Descripcion NVARCHAR(255),
    @CostoProduccion DECIMAL(10,2),
    @PrecioUnitario DECIMAL(10,2),
    @Estado BIT
AS
BEGIN
    UPDATE Productos
    SET
        CategoriaID = @CategoriaID,
        Nombre = @Nombre,
        Descripcion = @Descripcion,
        CostoProduccion = @CostoProduccion,
        PrecioUnitario = @PrecioUnitario,
        Estado = @Estado
    WHERE ProductoID = @ProductoID;
END
