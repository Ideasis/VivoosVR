CREATE TABLE [Core].[RoleToUserMaps] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_RoleToUserMaps_Id] DEFAULT (newid()) NOT NULL,
    [UserId]     UNIQUEIDENTIFIER CONSTRAINT [DF_RoleToUserMaps_OIDOrganization] DEFAULT ([dbo].[blankid]()) NOT NULL,
    [RoleCode]   NVARCHAR (100)   CONSTRAINT [DF_RoleToUserMaps_RoleCode] DEFAULT ('') NOT NULL,
    [ValidUntil] DATETIME         NULL,
    [EntryDate]  DATETIME         CONSTRAINT [DF_RoleToUserMaps_EntryDate] DEFAULT (getdate()) NOT NULL,
    [Available]  BIT              CONSTRAINT [DF_RoleToUserMaps_Available] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_RoleToUserMaps] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RoleToUserMaps_Roles] FOREIGN KEY ([RoleCode]) REFERENCES [Core].[Roles] ([Code]),
    CONSTRAINT [FK_RoleToUserMaps_Users] FOREIGN KEY ([UserId]) REFERENCES [Core].[Users] ([Id])
);

