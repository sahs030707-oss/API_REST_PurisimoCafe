

-- ==========================================================================
-- 5) PROCEDIMIENTOS ALMACENADOS: COMPRAS (mismo patrón que Ventas)
-- ==========================================================================

-- Inserta el encabezado de la compra y devuelve el CompraID generado
CREATE PROCEDURE [dbo].[Sp_InsertarCompra]
    @ProveedorID INT = NULL,
    @UsuarioID INT = NULL,
    @MetodoPago NVARCHAR(50),
    @Total DECIMAL(10,2),
    @FechaCompra DATETIME,
    @CompraID INT OUTPUT
AS
BEGIN
    INSERT INTO Compras (ProveedorID, UsuarioID, MetodoPago, Total, FechaCompra)
    VALUES (@ProveedorID, @UsuarioID, @MetodoPago, @Total, @FechaCompra);

    SET @CompraID = SCOPE_IDENTITY();
END
