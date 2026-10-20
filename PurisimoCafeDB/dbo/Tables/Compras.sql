CREATE TABLE [dbo].[Compras] (
    [CompraID]    INT             IDENTITY (1, 1) NOT NULL,
    [ProveedorID] INT             NULL,
    [UsuarioID]   INT             NULL,
    [MetodoPago]  NVARCHAR (50)   NULL,
    [Total]       DECIMAL (10, 2) NOT NULL,
    [FechaCompra] DATETIME        CONSTRAINT [DF_Compras_FechaCompra] DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([CompraID] ASC),
    CONSTRAINT [CK_Compras_Total] CHECK ([Total]>=(0)),
    FOREIGN KEY ([ProveedorID]) REFERENCES [dbo].[Proveedores] ([ProveedorID]),
    FOREIGN KEY ([UsuarioID]) REFERENCES [dbo].[Usuarios] ([UsuarioID])
);

