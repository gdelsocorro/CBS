CREATE TABLE [dbo].[Members] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (MAX) NOT NULL,
    [LastName]  NVARCHAR (MAX) NOT NULL,
    [IsActive]  BIT            NOT NULL,
    [Area]      INT            NOT NULL,
    [Email]     NVARCHAR (MAX) DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([Id] ASC)
);

