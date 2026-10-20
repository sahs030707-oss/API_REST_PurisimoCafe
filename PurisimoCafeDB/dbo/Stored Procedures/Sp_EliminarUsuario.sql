
--ELIMINAR
CREATE PROCEDURE [dbo].[Sp_EliminarUsuario]
    @UsuarioID INT
AS
BEGIN
    UPDATE Usuarios
    SET Estado = 0
    WHERE UsuarioID = @UsuarioID
END
