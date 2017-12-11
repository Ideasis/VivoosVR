CREATE TABLE [Vivos].[Assets] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_Assets_Id] DEFAULT (newid()) NOT NULL,
    [GroupId]     UNIQUEIDENTIFIER NOT NULL,
    [Name]        VARCHAR (100)    NOT NULL,
    [Description] VARCHAR (500)    NOT NULL,
    [Available]   BIT              CONSTRAINT [DF_Assets_Available] DEFAULT ((1)) NOT NULL,
    [Url]         VARCHAR (1000)   CONSTRAINT [DF_Assets_Url] DEFAULT ('') NOT NULL,
    [Exe]         VARCHAR (50)     CONSTRAINT [DF_Assets_Exe] DEFAULT ('') NOT NULL,
    [TimeStemp]   ROWVERSION       NOT NULL,
    [EntryDate]   DATETIME         CONSTRAINT [DF_Assets_EntryDate] DEFAULT (getdate()) NOT NULL,
    [ModifyDate]  DATETIME         CONSTRAINT [DF_Assets_ModifyDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Assets_AssetGroups] FOREIGN KEY ([GroupId]) REFERENCES [Vivos].[AssetGroups] ([Id])
);

