
CREATE PROCEDURE [dbo].[Sp_EliminarProveedor]
    @ProveedorID INT
AS
BEGIN
    UPDATE Proveedores
    SET Estado = 0
    WHERE ProveedorID = @ProveedorID
END
