--OBTENER MOVIMIENTOS (mermas)
CREATE PROCEDURE [dbo].[Sp_ObtenerMovimientos]
AS
BEGIN
    SELECT
        m.MovimientoID,
        m.ProductoID,
        p.Nombre,
        m.Cantidad,
        m.Motivo,
        m.Fecha
    FROM MovimientosInventario m
    INNER JOIN Productos p
        ON m.ProductoID = p.ProductoID
    ORDER BY m.Fecha DESC
END