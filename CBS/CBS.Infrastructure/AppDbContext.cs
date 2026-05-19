using CBS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CBS.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<Fund> Funds => Set<Fund>();
    public DbSet<Admin> Admins => Set<Admin>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Expense>()
            .HasOne(e => e.Vendor)
            .WithMany(v => v.Expenses)
            .HasForeignKey(e => e.VendorId);

        builder.Entity<Fund>()
            .HasOne(f => f.Member)
            .WithMany(m => m.Contributions)
            .HasForeignKey(f => f.MemberId);
    }
}
