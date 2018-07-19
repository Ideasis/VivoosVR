CREATE TABLE [Core].[Modules] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF_Modules_Id] DEFAULT (newid()) NOT NULL,
    [Description]  NVARCHAR (500)   CONSTRAINT [DF_Modules_Description] DEFAULT ('') NOT NULL,
    [AssemblyPath] VARCHAR (1000)   NOT NULL,
    [EntryDate]    DATETIME         CONSTRAINT [DF_Modules_EntryDate] DEFAULT (getdate()) NOT NULL,
    [Available]    BIT              CONSTRAINT [DF_Modules_Available] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED ([Id] ASC)
);

