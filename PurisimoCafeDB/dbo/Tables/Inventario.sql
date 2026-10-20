CREATE TABLE [dbo].[Inventario] (
    [InventarioID] INT      IDENTITY (1, 1) NOT NULL,
    [ProductoID]   INT      NOT NULL,
    [StockActual]  INT      DEFAULT ((0)) NOT NULL,
    [Fecha]        DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([InventarioID] ASC),
    CHECK ([StockActual]>=(0)),
    FOREIGN KEY ([ProductoID]) REFERENCES [dbo].[Productos] ([ProductoID]),
    UNIQUE NONCLUSTERED ([ProductoID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Inventario_ProductoID]
    ON [dbo].[Inventario]([ProductoID] ASC);

