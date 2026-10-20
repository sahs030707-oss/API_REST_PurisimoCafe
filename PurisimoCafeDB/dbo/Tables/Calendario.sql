CREATE TABLE [dbo].[Calendario] (
    [Fecha]           DATE          NOT NULL,
    [Anio]            INT           NULL,
    [Trimestre]       INT           NULL,
    [NombreTrimestre] NVARCHAR (6)  NULL,
    [Mes]             INT           NULL,
    [NombreMes]       NVARCHAR (20) NULL,
    [Semana]          INT           NULL,
    [DiaSemana]       INT           NULL,
    [NombreDia]       NVARCHAR (12) NULL,
    [EsFestivo]       BIT           DEFAULT ((0)) NULL,
    [EsFinSemana]     BIT           NULL,
    PRIMARY KEY CLUSTERED ([Fecha] ASC)
);

