using CBS.Domain.Enums;
using CBS.Domain.Models;

namespace CBS.Shared.ViewModels;

public class ExpenseViewModel
{
    public int            Id                 { get; set; }
    public int            VendorId           { get; set; }
    public DateTime?      Date               { get; set; } = DateTime.Today;
    public string         Description        { get; set; } = string.Empty;
    public decimal        Amount             { get; set; }
    public ExpenseTypes   ExpenseType        { get; set; }
    public string?        ReceiptImage       { get; set; }
    public bool           IsRecurring        { get; set; }
    public RecurrenceType RecurrenceType     { get; set; } = RecurrenceType.Monthly;
    public DateTime?      RecurrenceEndsOn   { get; set; }
    public int?           RecurrenceSourceId { get; set; }

    public static ExpenseViewModel From(Expense e) => new()
    {
        Id                 = e.Id,
        VendorId           = e.VendorId,
        Date               = e.ExpenseDate,
        Description        = e.Description,
        Amount             = e.Amount,
        ExpenseType        = e.ExpenseType,
        ReceiptImage       = e.ReceiptImage,
        IsRecurring        = e.IsRecurring,
        RecurrenceType     = e.RecurrenceType,
        RecurrenceEndsOn   = e.RecurrenceEndsOn,
        RecurrenceSourceId = e.RecurrenceSourceId
    };

    public Expense ToNew() => new()
    {
        VendorId         = VendorId,
        ExpenseDate      = Date ?? DateTime.Today,
        Description      = Description.Trim(),
        Amount           = Amount,
        ExpenseType      = ExpenseType,
        ReceiptImage     = ReceiptImage,
        IsRecurring      = IsRecurring,
        RecurrenceType   = RecurrenceType,
        RecurrenceEndsOn = RecurrenceEndsOn
    };

    public void ApplyTo(Expense e)
    {
        e.VendorId         = VendorId;
        e.ExpenseDate      = Date ?? DateTime.Today;
        e.Description      = Description.Trim();
        e.Amount           = Amount;
        e.ExpenseType      = ExpenseType;
        e.ReceiptImage     = ReceiptImage;
        e.IsRecurring      = IsRecurring;
        e.RecurrenceType   = RecurrenceType;
        e.RecurrenceEndsOn = RecurrenceEndsOn;
    }
}
