


-- PROCEDIMIENTOS ALMACENADOS
--OBTENER
CREATE PROCEDURE [dbo].[Sp_ObtenerUsuario]
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
    WHERE u.Estado = 1
END
