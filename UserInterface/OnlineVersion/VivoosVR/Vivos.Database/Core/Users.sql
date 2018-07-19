CREATE TABLE [Core].[Users] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_Users_Id] DEFAULT (newid()) NOT NULL,
    [GroupId]    UNIQUEIDENTIFIER NOT NULL,
    [ExpireDate] DATETIME         NULL,
    [Picture]    IMAGE            NULL,
    [Password]   NVARCHAR (100)   NOT NULL,
    [Available]  BIT              CONSTRAINT [DF_Users_Available] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_Consumers] FOREIGN KEY ([Id]) REFERENCES [Core].[Consumers] ([Id]),
    CONSTRAINT [FK_Users_Groups] FOREIGN KEY ([GroupId]) REFERENCES [Core].[Groups] ([Id])
);

