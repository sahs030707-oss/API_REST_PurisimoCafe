
--INSERTAR
CREATE PROCEDURE [dbo].[Sp_InsertarProducto]
    @CategoriaID INT,
    @Nombre NVARCHAR(60),
    @Descripcion NVARCHAR(255),
    @CostoProduccion DECIMAL(10,2),
    @PrecioUnitario DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Productos(CategoriaID, Nombre, Descripcion, CostoProduccion, PrecioUnitario)
    VALUES ( @CategoriaID, @Nombre, @Descripcion, @CostoProduccion, @PrecioUnitario);
END
