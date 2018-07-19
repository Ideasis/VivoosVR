CREATE TABLE [Vivos].[AssetDefaults] (
    [Id]             UNIQUEIDENTIFIER CONSTRAINT [DF_AssetDefaults_Id] DEFAULT (newid()) NOT NULL,
    [AssetId]        UNIQUEIDENTIFIER CONSTRAINT [DF_AssetDefaults_AssetId] DEFAULT ('73f2c995-194d-4103-b197-00a490f10a52') NOT NULL,
    [OnCommandText]  VARCHAR (100)    CONSTRAINT [DF_AssetDefaults_OnCommandText] DEFAULT ('') NOT NULL,
    [OnCommandName]  VARCHAR (100)    CONSTRAINT [DF_AssetDefaults_OnCommandName] DEFAULT ('') NOT NULL,
    [OffCommandText] VARCHAR (100)    CONSTRAINT [DF_AssetDefaults_OffCommandText] DEFAULT ('') NOT NULL,
    [OffCommandName] VARCHAR (100)    CONSTRAINT [DF_AssetDefaults_OffCommandName] DEFAULT ('') NOT NULL,
    [Description]    VARCHAR (500)    CONSTRAINT [DF_AssetDefaults_Description] DEFAULT ('') NOT NULL,
    [Step]           TINYINT          CONSTRAINT [DF_AssetDefaults_Step] DEFAULT ((0)) NOT NULL,
    [DefaultValue]   BIT              CONSTRAINT [DF_AssetDefaults_DefaultValue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AssetDefaults] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AssetDefaults_Assets] FOREIGN KEY ([AssetId]) REFERENCES [Vivos].[Assets] ([Id])
);

