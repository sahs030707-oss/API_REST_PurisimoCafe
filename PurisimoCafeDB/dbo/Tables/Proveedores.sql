CREATE TABLE [dbo].[Proveedores] (
    [ProveedorID] INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]      NVARCHAR (100) NOT NULL,
    [Telefono]    NVARCHAR (20)  NULL,
    [Direccion]   NVARCHAR (255) NULL,
    [Estado]      BIT            CONSTRAINT [DF_Proveedores_Estado] DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProveedorID] ASC)
);

