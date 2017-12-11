CREATE TABLE [Core].[Roles] (
    [Code]        NVARCHAR (100) NOT NULL,
    [Description] NVARCHAR (500) NOT NULL,
    [Available]   BIT            NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Code] ASC)
);

