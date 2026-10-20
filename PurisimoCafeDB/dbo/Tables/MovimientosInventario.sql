CREATE TABLE [dbo].[MovimientosInventario] (
    [MovimientoID] INT            IDENTITY (1, 1) NOT NULL,
    [ProductoID]   INT            NOT NULL,
    [Cantidad]     INT            NOT NULL,
    [Motivo]       NVARCHAR (255) NOT NULL,
    [Fecha]        DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([MovimientoID] ASC),
    CHECK ([Cantidad]>(0)),
    FOREIGN KEY ([ProductoID]) REFERENCES [dbo].[Productos] ([ProductoID])
);


GO
CREATE NONCLUSTERED INDEX [IX_MovimientosInventario_ProductoID]
    ON [dbo].[MovimientosInventario]([ProductoID] ASC);

