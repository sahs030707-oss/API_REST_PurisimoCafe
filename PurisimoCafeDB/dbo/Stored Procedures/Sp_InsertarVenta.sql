CREATE PROCEDURE [dbo].[Sp_InsertarVenta]
    @FechaVenta DATETIME,
    @Total      DECIMAL(10,2),
    @UsuarioID  INT = NULL,
    @VentaID    INT OUTPUT
AS
BEGIN
    INSERT INTO Ventas (FechaVenta, Total, UsuarioID)
    VALUES (@FechaVenta, @Total, @UsuarioID);

    SET @VentaID = SCOPE_IDENTITY();
END
