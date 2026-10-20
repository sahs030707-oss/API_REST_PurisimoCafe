CREATE TABLE [dbo].[Ventas] (
    [VentaID]    INT             IDENTITY (1, 1) NOT NULL,
    [UsuarioID]  INT             NULL,
    [FechaVenta] DATETIME        NOT NULL,
    [Total]      DECIMAL (10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([VentaID] ASC),
    CONSTRAINT [CK_Ventas_Total] CHECK ([Total]>=(0)),
    FOREIGN KEY ([UsuarioID]) REFERENCES [dbo].[Usuarios] ([UsuarioID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Ventas_UsuarioID]
    ON [dbo].[Ventas]([UsuarioID] ASC);

