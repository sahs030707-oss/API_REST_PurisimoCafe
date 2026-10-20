
--ELIMINAR
CREATE PROCEDURE [dbo].[Sp_EliminarProducto]
    @ProductoID INT
AS
BEGIN
    UPDATE Productos
    SET Estado = 0
    WHERE ProductoID = @ProductoID;
END
