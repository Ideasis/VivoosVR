CREATE TABLE [Vivos].[Problems] (
    [Id]                 UNIQUEIDENTIFIER CONSTRAINT [DF_Problems_Id] DEFAULT (newid()) NOT NULL,
    [PatientId]          UNIQUEIDENTIFIER NOT NULL,
    [Subject]            VARCHAR (200)    NOT NULL,
    [SymptomDescription] VARCHAR (MAX)    NOT NULL,
    [SymptomType]        VARCHAR (100)    NOT NULL,
    [SymptomStartDate]   DATE             NOT NULL,
    [SymptomResults]     VARCHAR (MAX)    NOT NULL,
    [IGDPoints]          INT              NOT NULL,
    [Avoidance]          VARCHAR (MAX)    NULL,
    [Precaution]         VARCHAR (MAX)    NULL,
    [History]            VARCHAR (MAX)    NULL,
    [MinFearPlace]       VARCHAR (200)    NULL,
    [LowFearPlace]       VARCHAR (200)    NULL,
    [MediumFearPlace]    VARCHAR (200)    NULL,
    [HighFearPlace]      VARCHAR (200)    NULL,
    [MaxFearPlace]       VARCHAR (200)    NULL,
    [EntryDate]          DATETIME         NOT NULL,
    CONSTRAINT [PK_Problems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Problems_Patients] FOREIGN KEY ([PatientId]) REFERENCES [Vivos].[Patients] ([Id])
);

