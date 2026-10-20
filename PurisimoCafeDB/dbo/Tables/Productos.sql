CREATE TABLE [dbo].[Productos] (
    [ProductoID]      INT             IDENTITY (1, 1) NOT NULL,
    [CategoriaID]     INT             NOT NULL,
    [Nombre]          NVARCHAR (60)   NOT NULL,
    [Descripcion]     NVARCHAR (255)  NULL,
    [Estado]          BIT             DEFAULT ((1)) NULL,
    [CostoProduccion] DECIMAL (10, 2) NULL,
    [PrecioUnitario]  DECIMAL (10, 2) NULL,
    PRIMARY KEY CLUSTERED ([ProductoID] ASC),
    CHECK ([CostoProduccion]>=(0)),
    CHECK ([PrecioUnitario]>=(0)),
    FOREIGN KEY ([CategoriaID]) REFERENCES [dbo].[Categorias] ([CategoriaID]),
    UNIQUE NONCLUSTERED ([Nombre] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Productos_CategoriaID]
    ON [dbo].[Productos]([CategoriaID] ASC);

