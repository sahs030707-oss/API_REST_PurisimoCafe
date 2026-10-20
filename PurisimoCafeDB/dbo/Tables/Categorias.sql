CREATE TABLE [dbo].[Categorias] (
    [CategoriaID] INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]      NVARCHAR (50)  NOT NULL,
    [Descripcion] NVARCHAR (255) NULL,
    [Estado]      BIT            DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([CategoriaID] ASC),
    UNIQUE NONCLUSTERED ([Nombre] ASC)
);

