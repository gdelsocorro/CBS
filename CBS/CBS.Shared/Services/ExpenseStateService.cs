using CBS.Domain.Enums;
using CBS.Domain.Models;
using CBS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CBS.Shared.Services;

public class ExpenseStateService
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    private readonly List<Expense> _expenses;

    public ExpenseStateService(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
        using var db = factory.CreateDbContext();
        _expenses = db.Expenses.OrderBy(e => e.Id).ToList();
    }

    public IReadOnlyList<Expense> Expenses => _expenses;

    public void Add(Expense expense)
    {
        expense.Id = 0;
        using var db = _factory.CreateDbContext();
        db.Expenses.Add(expense);
        db.SaveChanges();
        _expenses.Add(expense);
    }

    public void Remove(Expense expense)
    {
        using var db = _factory.CreateDbContext();
        db.Expenses.Where(e => e.Id == expense.Id).ExecuteDelete();
        _expenses.Remove(expense);
    }

    public void Update(Expense expense)
    {
        using var db = _factory.CreateDbContext();
        db.Expenses.Update(expense);
        db.SaveChanges();
    }

    public decimal TotalByType(ExpenseTypes type, int? year = null, int? month = null) =>
        Filter(year, month).Where(e => e.ExpenseType == type).Sum(e => e.Amount);

    public decimal TotalExpenses(int? year = null, int? month = null) =>
        Filter(year, month).Sum(e => e.Amount);

    private IEnumerable<Expense> Filter(int? year, int? month) =>
        _expenses.Where(e =>
            (year  == null || e.ExpenseDate.Year  == year) &&
            (month == null || e.ExpenseDate.Month == month));
}
