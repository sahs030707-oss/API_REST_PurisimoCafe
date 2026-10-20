
CREATE PROCEDURE [dbo].[Sp_ObtenerCompras]
AS
BEGIN
    SELECT
        c.CompraID,
        c.ProveedorID,
        pr.Nombre AS NombreProveedor,
        c.UsuarioID,
        u.Nombre AS NombreUsuario,
        c.MetodoPago,
        c.Total,
        c.FechaCompra
    FROM Compras c
    LEFT JOIN Proveedores pr ON c.ProveedorID = pr.ProveedorID
    LEFT JOIN Usuarios u     ON c.UsuarioID   = u.UsuarioID
    ORDER BY c.FechaCompra DESC
END
