CREATE TABLE [dbo].[DetalleVenta] (
    [DetalleVentaID] INT             IDENTITY (1, 1) NOT NULL,
    [VentaID]        INT             NOT NULL,
    [ProductoID]     INT             NOT NULL,
    [NombreProducto] NVARCHAR (60)   NULL,
    [PrecioUnitario] DECIMAL (10, 2) NOT NULL,
    [Cantidad]       INT             NOT NULL,
    [SubTotal]       DECIMAL (10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([DetalleVentaID] ASC),
    CHECK ([Cantidad]>(0)),
    CHECK ([PrecioUnitario]>=(0)),
    CHECK ([SubTotal]>=(0)),
    FOREIGN KEY ([ProductoID]) REFERENCES [dbo].[Productos] ([ProductoID]),
    FOREIGN KEY ([VentaID]) REFERENCES [dbo].[Ventas] ([VentaID])
);


GO
CREATE NONCLUSTERED INDEX [IX_DetalleVenta_ProductoID]
    ON [dbo].[DetalleVenta]([ProductoID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DetalleVenta_VentaID]
    ON [dbo].[DetalleVenta]([VentaID] ASC);

