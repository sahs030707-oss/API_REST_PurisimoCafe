--OBTENER TODOS (activos e inactivos)
CREATE PROCEDURE [dbo].[Sp_ObtenerTodosUsuarios]
AS
BEGIN
    SELECT
        u.UsuarioID,
        u.Nombre,
        u.Correo,
        u.RolID,
        r.NombreRol,
        u.Estado
    FROM Usuarios u
    INNER JOIN Roles r
        ON u.RolID = r.RolID
END
