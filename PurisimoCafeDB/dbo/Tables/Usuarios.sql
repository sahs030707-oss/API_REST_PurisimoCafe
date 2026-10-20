CREATE TABLE [dbo].[Usuarios] (
    [UsuarioID]  INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]     NVARCHAR (60)  NOT NULL,
    [Correo]     NVARCHAR (100) NOT NULL,
    [Contraseña] NVARCHAR (255) NOT NULL,
    [Estado]     BIT            DEFAULT ((1)) NULL,
    [RolID]      INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([UsuarioID] ASC),
    FOREIGN KEY ([RolID]) REFERENCES [dbo].[Roles] ([RolID])
);

