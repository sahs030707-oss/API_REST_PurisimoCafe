
--ACTUALIZAR
CREATE PROCEDURE [dbo].[Sp_ActualizarUsuario]
	@UsuarioID INT ,
	@Nombre NVARCHAR(100),
	@Correo NVARCHAR(100),
	@Contraseña NVARCHAR(255) = NULL,
	@RolID INT

AS
BEGIN
	UPDATE Usuarios
	SET Nombre = @Nombre,
		Correo = @Correo,
		Contraseña = ISNULL(NULLIF(@Contraseña, ''), Contraseña),
		RolID = @RolID
	WHERE UsuarioID = @UsuarioID;
END
