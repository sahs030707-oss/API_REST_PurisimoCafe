

-- ==========================================================================
-- 4) PROCEDIMIENTOS ALMACENADOS: PROVEEDORES (mismo patrón que Productos)
-- ==========================================================================

CREATE PROCEDURE [dbo].[Sp_ObtenerProveedores]
AS
BEGIN
    SELECT ProveedorID, Nombre, Telefono, Direccion, Estado
    FROM Proveedores
    WHERE Estado = 1
END
