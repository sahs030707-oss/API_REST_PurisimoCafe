
CREATE PROCEDURE [dbo].[Sp_ActualizarProveedor]
    @ProveedorID INT,
    @Nombre NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Direccion NVARCHAR(255),
    @Estado BIT
AS
BEGIN
    UPDATE Proveedores
    SET Nombre = @Nombre,
        Telefono = @Telefono,
        Direccion = @Direccion,
        Estado = @Estado
    WHERE ProveedorID = @ProveedorID
END
