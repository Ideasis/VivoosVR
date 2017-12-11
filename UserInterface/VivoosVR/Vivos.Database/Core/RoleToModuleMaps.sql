CREATE TABLE [Core].[RoleToModuleMaps] (
    [Id]        UNIQUEIDENTIFIER CONSTRAINT [DF_RoleToModuleMaps_Id] DEFAULT (newid()) NOT NULL,
    [RoleCode]  NVARCHAR (100)   NOT NULL,
    [ModuleId]  UNIQUEIDENTIFIER NOT NULL,
    [Available] BIT              CONSTRAINT [DF_RoleToModuleMaps_Available] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_RoleToModuleMaps] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RoleToModuleMaps_Modules] FOREIGN KEY ([ModuleId]) REFERENCES [Core].[Modules] ([Id]),
    CONSTRAINT [FK_RoleToModuleMaps_Roles] FOREIGN KEY ([RoleCode]) REFERENCES [Core].[Roles] ([Code])
);

