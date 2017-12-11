CREATE TABLE [Core].[Groups] (
    [Id]       UNIQUEIDENTIFIER CONSTRAINT [DF_Groups_Id] DEFAULT (newid()) NOT NULL,
    [BranchId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Groups_Branches] FOREIGN KEY ([BranchId]) REFERENCES [Core].[Branches] ([Id]),
    CONSTRAINT [FK_Groups_Consumers] FOREIGN KEY ([Id]) REFERENCES [Core].[Consumers] ([Id])
);

