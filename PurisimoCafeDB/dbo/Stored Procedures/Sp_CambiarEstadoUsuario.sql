--ACTIVAR O DESACTIVAR
CREATE PROCEDURE [dbo].[Sp_CambiarEstadoUsuario]
    @UsuarioID INT,
    @Estado BIT
AS
BEGIN
    UPDATE Usuarios
    SET Estado = @Estado
    WHERE UsuarioID = @UsuarioID
END
