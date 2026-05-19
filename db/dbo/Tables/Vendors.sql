CREATE TABLE [dbo].[Vendors] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [Address]   NVARCHAR (MAX) NOT NULL,
    [DateAdded] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Vendors] PRIMARY KEY CLUSTERED ([Id] ASC)
);

