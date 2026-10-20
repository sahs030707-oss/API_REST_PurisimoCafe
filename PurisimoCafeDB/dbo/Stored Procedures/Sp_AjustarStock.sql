
--ajustar stock manualmente si se desea ( En este caso por que estamos trabajando con un solo modulo)
CREATE PROCEDURE [dbo].[Sp_AjustarStock]
    @ProductoID INT,
    @NuevoStock INT
AS
BEGIN
    IF @NuevoStock < 0
        RETURN;

    UPDATE Inventario
    SET StockActual = @NuevoStock,
        Fecha = GETDATE()
    WHERE ProductoID = @ProductoID
END
