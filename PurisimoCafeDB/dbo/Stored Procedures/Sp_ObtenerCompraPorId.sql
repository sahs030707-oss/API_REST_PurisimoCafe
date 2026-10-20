
-- Devuelve el encabezado de una compra y, en un segundo resultado, su detalle
CREATE PROCEDURE [dbo].[Sp_ObtenerCompraPorId]
    @CompraID INT
AS
BEGIN
    -- Primer resultado: encabezado
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
    WHERE c.CompraID = @CompraID

    -- Segundo resultado: detalle
    SELECT
        dc.DetalleCompraID,
        dc.CompraID,
        dc.ProductoID,
        p.Nombre AS NombreProducto,
        dc.PrecioUnitario,
        dc.Cantidad,
        dc.Subtotal
    FROM DetalleCompra dc
    INNER JOIN Productos p ON dc.ProductoID = p.ProductoID
    WHERE dc.CompraID = @CompraID
END
