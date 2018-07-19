CREATE TABLE [Vivos].[Patients] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_Patients_Id] DEFAULT (newid()) NOT NULL,
    [DoctorId]   UNIQUEIDENTIFIER NOT NULL,
    [Age]        TINYINT          NOT NULL,
    [IsApproved] BIT              CONSTRAINT [DF_Patients_IsApproved] DEFAULT ((0)) NOT NULL,
    [EntryDate]  DATETIME         CONSTRAINT [DF_Patients_EntryDate] DEFAULT (getdate()) NOT NULL,
    [Code]       VARCHAR (100)    NOT NULL,
    CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Patients_Users] FOREIGN KEY ([DoctorId]) REFERENCES [Core].[Users] ([Id]),
    CONSTRAINT [IX_Patients_DoctorAndCodeUniqueness] UNIQUE NONCLUSTERED ([DoctorId] ASC, [Code] ASC)
);

