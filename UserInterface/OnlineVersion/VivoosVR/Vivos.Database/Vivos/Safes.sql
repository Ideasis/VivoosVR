CREATE TABLE [Vivos].[Safes] (
    [Id]        UNIQUEIDENTIFIER CONSTRAINT [DF_Safes_Id] DEFAULT (newid()) NOT NULL,
    [CompanyId] UNIQUEIDENTIFIER NOT NULL,
    [AssetId]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Safes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Safes_Assets] FOREIGN KEY ([AssetId]) REFERENCES [Vivos].[Assets] ([Id]),
    CONSTRAINT [FK_Safes_Companies] FOREIGN KEY ([CompanyId]) REFERENCES [Core].[Companies] ([Id])
);

