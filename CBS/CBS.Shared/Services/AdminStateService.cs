using CBS.Domain.Models;
using CBS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CBS.Shared.Services;

public class AdminStateService
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    private readonly List<Admin> _admins;

    public AdminStateService(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
        using var db = factory.CreateDbContext();
        _admins = db.Admins.OrderBy(a => a.Id).ToList();
    }

    public IReadOnlyList<Admin> Admins => _admins;

    public void Add(Admin admin)
    {
        admin.Id = 0;
        using var db = _factory.CreateDbContext();
        db.Admins.Add(admin);
        db.SaveChanges();
        _admins.Add(admin);
    }

    public void Remove(Admin admin)
    {
        using var db = _factory.CreateDbContext();
        db.Admins.Where(a => a.Id == admin.Id).ExecuteDelete();
        _admins.Remove(admin);
    }

    public void Update(Admin admin)
    {
        using var db = _factory.CreateDbContext();
        db.Admins.Update(admin);
        db.SaveChanges();
    }

    public Admin? FindByUsername(string username) =>
        _admins.FirstOrDefault(a => a.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
}
