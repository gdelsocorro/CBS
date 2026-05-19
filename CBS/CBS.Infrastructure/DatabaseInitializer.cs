using Microsoft.EntityFrameworkCore;

namespace CBS.Infrastructure;

public static class DatabaseInitializer
{
    public static void Initialize(AppDbContext db)
    {
        // For databases created before EF migrations were introduced:
        // create the history table and stamp the baseline migration so Migrate()
        // doesn't try to recreate tables that already exist.
        if (db.Database.CanConnect())
        {
            var initial = db.Database.GetMigrations().FirstOrDefault();
            if (initial is not null)
            {
                db.Database.ExecuteSqlRaw($@"
                    IF OBJECT_ID('__EFMigrationsHistory') IS NULL
                    BEGIN
                        CREATE TABLE [__EFMigrationsHistory] (
                            [MigrationId]    nvarchar(150) NOT NULL,
                            [ProductVersion] nvarchar(32)  NOT NULL,
                            CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
                        );
                        IF OBJECT_ID('Members') IS NOT NULL
                            INSERT INTO [__EFMigrationsHistory] VALUES ('{initial}', '10.0.3');
                    END");
            }
        }

        db.Database.Migrate();
    }
}
