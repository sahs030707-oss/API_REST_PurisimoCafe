
-- Inserta una línea del detalle de la compra
CREATE PROCEDURE [dbo].[Sp_InsertarDetalleCompra]
    @CompraID INT,
    @ProductoID INT,
    @Cantidad INT,
    @PrecioUnitario DECIMAL(10,2),
    @Subtotal DECIMAL(10,2)
AS
BEGIN
    INSERT INTO DetalleCompra (CompraID, ProductoID, Cantidad, PrecioUnitario, Subtotal)
    VALUES (@CompraID, @ProductoID, @Cantidad, @PrecioUnitario, @Subtotal);
END
