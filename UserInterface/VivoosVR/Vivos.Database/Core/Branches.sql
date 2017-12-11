CREATE TABLE [Core].[Branches] (
    [Id]        UNIQUEIDENTIFIER CONSTRAINT [DF_Branches_Id] DEFAULT (newid()) NOT NULL,
    [CompanyId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Branches] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Branches_Companies] FOREIGN KEY ([CompanyId]) REFERENCES [Core].[Companies] ([Id]),
    CONSTRAINT [FK_Branches_Consumers] FOREIGN KEY ([Id]) REFERENCES [Core].[Consumers] ([Id])
);

