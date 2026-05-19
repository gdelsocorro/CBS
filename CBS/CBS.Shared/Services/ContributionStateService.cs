using CBS.Domain.Enums;
using CBS.Domain.Models;
using CBS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CBS.Shared.Services;

public class ContributionStateService
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    private readonly List<Fund> _contributions;

    public ContributionStateService(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
        using var db = factory.CreateDbContext();
        _contributions = db.Funds.OrderBy(f => f.Id).ToList();
    }

    public IReadOnlyList<Fund> Contributions => _contributions;

    public void Add(Fund fund)
    {
        fund.Id = 0;
        using var db = _factory.CreateDbContext();
        db.Funds.Add(fund);
        db.SaveChanges();
        _contributions.Add(fund);
    }

    public void Remove(Fund fund)
    {
        using var db = _factory.CreateDbContext();
        db.Funds.Where(f => f.Id == fund.Id).ExecuteDelete();
        _contributions.Remove(fund);
    }

    public void Update(Fund fund)
    {
        using var db = _factory.CreateDbContext();
        db.Funds.Update(fund);
        db.SaveChanges();
    }

    public decimal TotalByType(FundTypes type, int? year = null, int? month = null) =>
        Filter(year, month).Where(c => c.ContributionType == type).Sum(c => c.Amount);

    public decimal TotalSources(int? year = null, int? month = null) =>
        Filter(year, month).Sum(c => c.Amount);

    private IEnumerable<Fund> Filter(int? year, int? month) =>
        _contributions.Where(c =>
            (year  == null || c.ContributionDate.Year  == year) &&
            (month == null || c.ContributionDate.Month == month));
}
