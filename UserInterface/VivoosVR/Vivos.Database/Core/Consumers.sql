CREATE TABLE [Core].[Consumers] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_Organizations_Id] DEFAULT (newid()) NOT NULL,
    [Code]        NVARCHAR (200)   CONSTRAINT [DF_Organizations_Identity] DEFAULT ('') NOT NULL,
    [Description] NVARCHAR (MAX)   CONSTRAINT [DF_Organizations_Description] DEFAULT ('') NOT NULL,
    [Email]       NVARCHAR (200)   CONSTRAINT [DF_Organizations_EMail] DEFAULT ('') NOT NULL,
    [EntryDate]   DATETIME         CONSTRAINT [DF_Organizations_EntryDate] DEFAULT ('') NOT NULL,
    [WindowsCode] VARCHAR (1000)   NULL,
    CONSTRAINT [PK_Organizations] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [IX_OrganizationCode]
    ON [Core].[Consumers]([Code] ASC);

