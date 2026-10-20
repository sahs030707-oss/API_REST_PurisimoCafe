
CREATE PROCEDURE [dbo].[Sp_InsertarProveedor]
    @Nombre NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Direccion NVARCHAR(255)
AS
BEGIN
    INSERT INTO Proveedores (Nombre, Telefono, Direccion)
    VALUES (@Nombre, @Telefono, @Direccion);
END
