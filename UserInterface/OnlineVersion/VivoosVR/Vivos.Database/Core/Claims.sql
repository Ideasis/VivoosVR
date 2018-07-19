CREATE TABLE [Core].[Claims] (
    [Id]        UNIQUEIDENTIFIER CONSTRAINT [DF_Securities_Id] DEFAULT (newid()) NOT NULL,
    [RoleCode]  NVARCHAR (100)   CONSTRAINT [DF_Securities_CodeRole] DEFAULT ('') NOT NULL,
    [Resource]  VARCHAR (450)    CONSTRAINT [DF_Securities_ResourceIdentifier] DEFAULT ('') NOT NULL,
    [Available] BIT              CONSTRAINT [DF_Securities_Available] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Claims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Claims_Roles] FOREIGN KEY ([RoleCode]) REFERENCES [Core].[Roles] ([Code])
);


GO
CREATE NONCLUSTERED INDEX [IX_Claims]
    ON [Core].[Claims]([RoleCode] ASC);

