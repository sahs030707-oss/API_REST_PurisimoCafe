

--procedimiento almacenado de MovimientoInventario
CREATE PROCEDURE [dbo].[Sp_InsertarMovimientoInventario]
    @ProductoID INT,
    @Cantidad INT,
    @Motivo NVARCHAR(255)
AS
BEGIN
    -- Evitar cantidades inválidas
    IF @Cantidad <= 0
    BEGIN
        RAISERROR('La cantidad debe ser mayor a 0.', 16, 1);
        RETURN;
    END

    -- Verificar stock suficiente
    IF NOT EXISTS (
        SELECT 1
        FROM Inventario
        WHERE ProductoID = @ProductoID
        AND StockActual >= @Cantidad
    )
    BEGIN
        RAISERROR('Stock insuficiente.', 16, 1);
        RETURN;
    END

    -- Registrar movimiento
    INSERT INTO MovimientosInventario
    (
        ProductoID,
        Cantidad,
        Motivo
    )
    VALUES
    (
        @ProductoID,
        @Cantidad,
        @Motivo
    );

    -- Disminuir stock
    UPDATE Inventario
    SET 
        StockActual = StockActual - @Cantidad,
        Fecha = GETDATE()
    WHERE ProductoID = @ProductoID;
END
