using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CBS.Infrastructure;

public static class DatabaseInitializer
{
    public static void Initialize(AppDbContext db)
    {
        var creator = db.Database.GetService<IRelationalDatabaseCreator>();

        if (!creator.Exists())
            creator.Create();

        if (!creator.HasTables())
            creator.CreateTables();

        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Members' AND COLUMN_NAME = 'Email')
            ALTER TABLE [Members] ADD [Email] nvarchar(max) NOT NULL DEFAULT '';");

        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Admins')
            CREATE TABLE [Admins] (
                [Id]           int           NOT NULL IDENTITY,
                [Username]     nvarchar(max) NOT NULL,
                [FirstName]    nvarchar(max) NOT NULL,
                [LastName]     nvarchar(max) NOT NULL,
                [IsActive]     bit           NOT NULL,
                [PasswordHash] nvarchar(max) NOT NULL,
                CONSTRAINT [PK_Admins] PRIMARY KEY ([Id])
            );");
    }
}
