CREATE TABLE [Core].[Companies] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Logo] IMAGE            NULL,
    CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Companies_Consumers] FOREIGN KEY ([Id]) REFERENCES [Core].[Consumers] ([Id])
);

