CREATE TABLE [Vivos].[AssetGroups] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_AssetGroups_Id] DEFAULT (newid()) NOT NULL,
    [Name]        NVARCHAR (50)    NOT NULL,
    [Description] NVARCHAR (200)   NOT NULL,
    CONSTRAINT [PK_AssetGroups] PRIMARY KEY CLUSTERED ([Id] ASC)
);

