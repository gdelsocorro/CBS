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
        GenerateRecurring();
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

    // Generates missing recurring entries up to today. Called on startup and
    // after saving a new recurring template so entries appear immediately.
    public void GenerateRecurring()
    {
        var templates = _expenses
            .Where(e => e.IsRecurring && e.RecurrenceSourceId == null)
            .ToList();

        foreach (var t in templates)
        {
            var latestDate = _expenses
                .Where(e => e.Id == t.Id || e.RecurrenceSourceId == t.Id)
                .Max(e => e.ExpenseDate);

            var next = t.RecurrenceType == RecurrenceType.Weekly
                ? latestDate.AddDays(7)
                : latestDate.AddMonths(1);

            while (next.Date <= DateTime.Today &&
                   (t.RecurrenceEndsOn == null || next.Date <= t.RecurrenceEndsOn.Value.Date))
            {
                Add(new Expense
                {
                    VendorId           = t.VendorId,
                    Amount             = t.Amount,
                    Description        = t.Description,
                    ExpenseDate        = next,
                    ExpenseType        = t.ExpenseType,
                    IsRecurring        = false,
                    RecurrenceType     = t.RecurrenceType,
                    RecurrenceSourceId = t.Id
                });

                next = t.RecurrenceType == RecurrenceType.Weekly
                    ? next.AddDays(7)
                    : next.AddMonths(1);
            }
        }
    }
}
