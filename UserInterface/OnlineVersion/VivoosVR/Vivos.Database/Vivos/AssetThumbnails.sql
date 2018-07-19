CREATE TABLE [Vivos].[AssetThumbnails] (
    [AssetId]   UNIQUEIDENTIFIER NOT NULL,
    [Thumbnail] IMAGE            NULL,
    CONSTRAINT [PK_AssetThumbnails] PRIMARY KEY CLUSTERED ([AssetId] ASC),
    CONSTRAINT [FK_AssetThumbnails_Assets] FOREIGN KEY ([AssetId]) REFERENCES [Vivos].[Assets] ([Id])
);

