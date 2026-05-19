CREATE TABLE [dbo].[Admins] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (MAX) NOT NULL,
    [FirstName]    NVARCHAR (MAX) NOT NULL,
    [LastName]     NVARCHAR (MAX) NOT NULL,
    [IsActive]     BIT            NOT NULL,
    [PasswordHash] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED ([Id] ASC)
);

