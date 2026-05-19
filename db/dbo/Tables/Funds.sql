CREATE TABLE [dbo].[Funds] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [MemberId]         INT             NOT NULL,
    [Amount]           DECIMAL (18, 2) NOT NULL,
    [ContributionType] INT             NOT NULL,
    [ContributionDate] DATETIME2 (7)   NOT NULL,
    [Month]            INT             NOT NULL,
    CONSTRAINT [PK_Funds] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Funds_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Funds_MemberId]
    ON [dbo].[Funds]([MemberId] ASC);

