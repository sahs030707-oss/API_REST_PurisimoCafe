
CREATE PROCEDURE [dbo].[Sp_ObtenerProveedorPorId]
    @ProveedorID INT
AS
BEGIN
    SELECT ProveedorID, Nombre, Telefono, Direccion, Estado
    FROM Proveedores
    WHERE ProveedorID = @ProveedorID
END
