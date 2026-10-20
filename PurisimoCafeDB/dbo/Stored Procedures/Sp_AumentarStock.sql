
--Aumentar o crear stock si no existe 
CREATE PROCEDURE [dbo].[Sp_AumentarStock]
    @ProductoID INT,
    @Cantidad INT
AS
BEGIN
    IF @Cantidad <= 0
        RETURN;

    IF EXISTS (SELECT 1 FROM Inventario WHERE ProductoID = @ProductoID)
    BEGIN
        UPDATE Inventario
        SET StockActual = StockActual + @Cantidad,
            Fecha = GETDATE()
        WHERE ProductoID = @ProductoID
    END
    ELSE
    BEGIN
        INSERT INTO Inventario (ProductoID, StockActual, Fecha)
        VALUES (@ProductoID, @Cantidad, GETDATE())
    END
END
