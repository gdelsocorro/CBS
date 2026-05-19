CREATE TABLE [dbo].[Expenses] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [VendorId]     INT             NOT NULL,
    [Amount]       DECIMAL (18, 2) NOT NULL,
    [Description]  NVARCHAR (MAX)  NOT NULL,
    [ExpenseDate]  DATETIME2 (7)   NOT NULL,
    [ExpenseType]  INT             NOT NULL,
    [ReceiptImage] NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_Expenses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Expenses_Vendors_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [dbo].[Vendors] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Expenses_VendorId]
    ON [dbo].[Expenses]([VendorId] ASC);

