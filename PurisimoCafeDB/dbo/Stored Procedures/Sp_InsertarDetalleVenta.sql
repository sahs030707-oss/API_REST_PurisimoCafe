CREATE PROCEDURE [dbo].[Sp_InsertarDetalleVenta]
    @VentaID        INT,
    @ProductoID     INT,
    @NombreProducto NVARCHAR(60),
    @PrecioUnitario DECIMAL(10,2),
    @Cantidad       INT,
    @Subtotal       DECIMAL(10,2)
AS
BEGIN
    IF @Cantidad <= 0
        RETURN;

    INSERT INTO DetalleVenta (VentaID, ProductoID, NombreProducto, PrecioUnitario, Cantidad, SubTotal)
    VALUES (@VentaID, @ProductoID, @NombreProducto, @PrecioUnitario, @Cantidad, @Subtotal);
END
