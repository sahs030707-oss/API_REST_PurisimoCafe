CREATE TABLE [dbo].[DetalleCompra] (
    [DetalleCompraID] INT             IDENTITY (1, 1) NOT NULL,
    [CompraID]        INT             NOT NULL,
    [ProductoID]      INT             NOT NULL,
    [Cantidad]        INT             NOT NULL,
    [PrecioUnitario]  DECIMAL (10, 2) NOT NULL,
    [Subtotal]        DECIMAL (10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([DetalleCompraID] ASC),
    CONSTRAINT [CK_DetalleCompra_Cantidad] CHECK ([Cantidad]>(0)),
    CONSTRAINT [CK_DetalleCompra_Precio] CHECK ([PrecioUnitario]>=(0)),
    CONSTRAINT [CK_DetalleCompra_Subtotal] CHECK ([Subtotal]>=(0)),
    FOREIGN KEY ([CompraID]) REFERENCES [dbo].[Compras] ([CompraID]),
    FOREIGN KEY ([ProductoID]) REFERENCES [dbo].[Productos] ([ProductoID])
);

