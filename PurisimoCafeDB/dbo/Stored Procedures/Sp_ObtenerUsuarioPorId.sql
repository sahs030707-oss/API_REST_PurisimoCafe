
--OBTENER POR ID
CREATE PROCEDURE [dbo].[Sp_ObtenerUsuarioPorId]
    @UsuarioID INT
AS
BEGIN
    SELECT 
		u.UsuarioID ,
		u.Nombre, 
		u.Correo, 
		u.RolID,
		r.NombreRol
    FROM Usuarios u
	INNER JOIN Roles r ON u.RolID = r.RolID
    WHERE UsuarioID = @UsuarioID
END
