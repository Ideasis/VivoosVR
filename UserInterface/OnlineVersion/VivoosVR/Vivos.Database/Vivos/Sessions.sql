CREATE TABLE [Vivos].[Sessions] (
    [Id]              UNIQUEIDENTIFIER CONSTRAINT [DF_Sessions_Id] DEFAULT (newid()) NOT NULL,
    [PatientId]       UNIQUEIDENTIFIER NOT NULL,
    [SessionDateTime] DATETIME         CONSTRAINT [DF_Sessions_SessionDateTime] DEFAULT (getdate()) NOT NULL,
    [Notes]           VARCHAR (MAX)    CONSTRAINT [DF_Sessions_Notes] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Sessions_Patients] FOREIGN KEY ([PatientId]) REFERENCES [Vivos].[Patients] ([Id])
);

