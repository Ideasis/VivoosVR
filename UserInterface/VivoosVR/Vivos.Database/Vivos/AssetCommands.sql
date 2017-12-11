CREATE TABLE [Vivos].[AssetCommands] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_AssetCommands_Id] DEFAULT (newid()) NOT NULL,
    [AssetId]     UNIQUEIDENTIFIER CONSTRAINT [DF_AssetCommands_AssetId] DEFAULT ('73f2c995-194d-4103-b197-00a490f10a52') NOT NULL,
    [CommandText] VARCHAR (100)    CONSTRAINT [DF_AssetCommands_CommandText] DEFAULT ('') NOT NULL,
    [Description] VARCHAR (500)    CONSTRAINT [DF_AssetCommands_Description] DEFAULT ('') NOT NULL,
    [Step]        TINYINT          CONSTRAINT [DF_AssetCommands_Step] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AssetCommands] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AssetCommands_Assets] FOREIGN KEY ([AssetId]) REFERENCES [Vivos].[Assets] ([Id])
);

