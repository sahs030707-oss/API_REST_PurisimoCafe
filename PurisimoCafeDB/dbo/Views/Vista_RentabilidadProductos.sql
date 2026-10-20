CREATE VIEW [dbo].[Vista_RentabilidadProductos] AS
SELECT
    p.ProductoID,
    p.Nombre                                                        AS Producto,
    c.Nombre                                                        AS Categoria,
    p.CostoProduccion,
    p.PrecioUnitario,
    p.PrecioUnitario - p.CostoProduccion                           AS MargenUnitario,
    CASE WHEN p.PrecioUnitario = 0 THEN 0
         ELSE CAST((p.PrecioUnitario - p.CostoProduccion)
                   * 100.0 / p.PrecioUnitario AS DECIMAL(5,2))
    END                                                            AS MargenUnitarioPct,
    ISNULL(SUM(dv.Cantidad), 0)                                    AS UnidadesVendidas,
    ISNULL(SUM(dv.SubTotal), 0)                                    AS IngresoTotal,
    ISNULL(SUM(p.CostoProduccion * dv.Cantidad), 0)               AS CostoTotal,
    ISNULL(SUM(dv.SubTotal), 0)
        - ISNULL(SUM(p.CostoProduccion * dv.Cantidad), 0)         AS GananciaNeta,
    CASE WHEN ISNULL(SUM(dv.SubTotal), 0) = 0 THEN 0
         ELSE CAST(
             (ISNULL(SUM(dv.SubTotal), 0)
                 - ISNULL(SUM(p.CostoProduccion * dv.Cantidad), 0))
             * 100.0 / ISNULL(SUM(dv.SubTotal), 0)
             AS DECIMAL(5,2))
    END                                                            AS MargenRealPct
FROM Productos p
LEFT JOIN DetalleVenta dv ON p.ProductoID = dv.ProductoID
INNER JOIN Categorias c   ON p.CategoriaID = c.CategoriaID
GROUP BY
    p.ProductoID, p.Nombre, c.Nombre,
    p.CostoProduccion, p.PrecioUnitario;

