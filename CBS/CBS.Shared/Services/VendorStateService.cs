using CBS.Domain.Models;
using CBS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CBS.Shared.Services;

public class VendorStateService
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    private readonly List<Vendor> _vendors;

    public VendorStateService(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
        using var db = factory.CreateDbContext();
        _vendors = db.Vendors.OrderBy(v => v.Id).ToList();
    }

    public IReadOnlyList<Vendor> Vendors     => _vendors.Where(v => v.IsActive).ToList();
    public IReadOnlyList<Vendor> AllVendors  => _vendors;

    public void Add(Vendor vendor)
    {
        vendor.Id = 0;
        vendor.IsActive = true;
        using var db = _factory.CreateDbContext();
        db.Vendors.Add(vendor);
        db.SaveChanges();
        _vendors.Add(vendor);
    }

    public void Deactivate(Vendor vendor)
    {
        vendor.IsActive = false;
        using var db = _factory.CreateDbContext();
        db.Vendors.Update(vendor);
        db.SaveChanges();
    }

    public void Restore(Vendor vendor)
    {
        vendor.IsActive = true;
        using var db = _factory.CreateDbContext();
        db.Vendors.Update(vendor);
        db.SaveChanges();
    }

    public void Update(Vendor vendor)
    {
        using var db = _factory.CreateDbContext();
        db.Vendors.Update(vendor);
        db.SaveChanges();
    }

    public string GetName(int vendorId) =>
        _vendors.FirstOrDefault(v => v.Id == vendorId)?.Name ?? $"Vendor #{vendorId}";
}
