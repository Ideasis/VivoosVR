CREATE TABLE [Core].[ConsumerProperties] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_OrganizationProperties_Id] DEFAULT (newid()) NOT NULL,
    [ConsumerId] UNIQUEIDENTIFIER CONSTRAINT [DF_OrganizationProperties_OIDOrganization] DEFAULT ([dbo].[blankid]()) NOT NULL,
    [Value]      NVARCHAR (MAX)   CONSTRAINT [DF_OrganizationProperties_Value] DEFAULT ('') NOT NULL,
    [Available]  BIT              CONSTRAINT [DF_OrganizationProperties_Available] DEFAULT ((1)) NOT NULL,
    [EntryDate]  DATETIME         CONSTRAINT [DF_OrganizationProperties_EntryDate] DEFAULT (getdate()) NOT NULL,
    [TypeNo]     INT              NOT NULL,
    CONSTRAINT [PK_ConsumerProperties] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsumerProperties_ConsumerPropertyTypes] FOREIGN KEY ([TypeNo]) REFERENCES [Parameter].[ConsumerPropertyTypes] ([No]),
    CONSTRAINT [FK_ConsumerProperties_Organizations] FOREIGN KEY ([ConsumerId]) REFERENCES [Core].[Consumers] ([Id])
);


GO
CREATE CLUSTERED INDEX [IX_ConsumerProperties]
    ON [Core].[ConsumerProperties]([ConsumerId] ASC);

