using System.Reflection;
using CBS.Domain.Models;
using CBS.Domain.Enums;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CBS.Shared.Services;

public static class PdfExportService
{
    static PdfExportService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    // ── Member contribution receipt ──────────────────────────────────────

    public static byte[] BuildMemberReceipt(Member member, IEnumerable<Fund> contributions, int? year)
    {
        var items = contributions.OrderByDescending(c => c.ContributionDate).ToList();
        var total = items.Sum(c => c.Amount);
        var byType = items
            .GroupBy(c => c.ContributionType)
            .Select(g => (Label: DisplayName(g.Key), Amount: g.Sum(c => c.Amount)))
            .ToList();

        return Document.Create(doc =>
        {
            doc.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(s => s.FontSize(10));

                // Header
                page.Header().Column(col =>
                {
                    col.Item().Row(r =>
                    {
                        r.RelativeItem().Column(c =>
                        {
                            c.Item().Text("CBS – Church Budgeting System").Bold().FontSize(15);
                            c.Item().Text("LAMI Church").FontSize(11).FontColor(Colors.Grey.Darken1);
                        });
                        r.ConstantItem(150).AlignRight().Column(c =>
                        {
                            c.Item().Text("Contribution Receipt").Bold().FontSize(11);
                            c.Item().Text(year.HasValue ? $"Year: {year}" : "All Years").FontSize(10).FontColor(Colors.Blue.Darken2);
                            c.Item().Text($"Generated: {DateTime.Now:MMM dd, yyyy}").FontSize(9).FontColor(Colors.Grey.Darken1);
                        });
                    });
                    col.Item().PaddingTop(8).BorderBottom(1).BorderColor(Colors.Grey.Lighten1);
                });

                // Content
                page.Content().PaddingTop(14).Column(col =>
                {
                    // Member summary card
                    col.Item().Background("#EBF3FC").Padding(10).Row(r =>
                    {
                        r.RelativeItem().Column(c =>
                        {
                            c.Item().Text("Member").FontSize(8).FontColor(Colors.Grey.Darken1);
                            c.Item().Text(member.FullName).Bold().FontSize(13);
                            c.Item().Text($"Area: {member.Area}" +
                                (string.IsNullOrEmpty(member.Email) ? "" : $"  |  {member.Email}"))
                                .FontSize(9).FontColor(Colors.Grey.Darken1);
                        });
                        r.ConstantItem(130).AlignRight().Column(c =>
                        {
                            c.Item().Text("Total").FontSize(8).FontColor(Colors.Grey.Darken1);
                            c.Item().Text($"PHP {total:N2}").Bold().FontSize(15).FontColor("#1565C0");
                            c.Item().Text($"{items.Count} record{(items.Count == 1 ? "" : "s")}").FontSize(9).FontColor(Colors.Grey.Darken1);
                        });
                    });

                    // Type breakdown
                    if (byType.Any())
                    {
                        col.Item().PaddingTop(8).Row(r =>
                        {
                            foreach (var g in byType)
                            {
                                r.AutoItem().PaddingRight(6)
                                    .Border(1).BorderColor(Colors.Grey.Lighten2).Padding(6).Column(c =>
                                    {
                                        c.Item().Text(g.Label).FontSize(8).FontColor(Colors.Grey.Darken1);
                                        c.Item().Text($"PHP {g.Amount:N2}").Bold().FontSize(10);
                                    });
                            }
                        });
                    }

                    col.Item().PaddingTop(12);

                    // Contributions table
                    col.Item().Table(t =>
                    {
                        t.ColumnsDefinition(c =>
                        {
                            c.ConstantColumn(40);
                            c.RelativeColumn(2);
                            c.RelativeColumn(3);
                            c.RelativeColumn(2);
                        });

                        t.Header(h =>
                        {
                            h.Cell().Background("#1565C0").Padding(5).Text("ID").Bold().FontColor(Colors.White).FontSize(9);
                            h.Cell().Background("#1565C0").Padding(5).Text("Date").Bold().FontColor(Colors.White).FontSize(9);
                            h.Cell().Background("#1565C0").Padding(5).Text("Type").Bold().FontColor(Colors.White).FontSize(9);
                            h.Cell().Background("#1565C0").Padding(5).AlignRight().Text("Amount").Bold().FontColor(Colors.White).FontSize(9);
                        });

                        for (int i = 0; i < items.Count; i++)
                        {
                            var c = items[i];
                            var bg = i % 2 == 0 ? Colors.White : Colors.Grey.Lighten5;
                            t.Cell().Background(bg).Padding(5).Text(c.Id.ToString()).FontSize(9);
                            t.Cell().Background(bg).Padding(5).Text(c.ContributionDate.ToString("MMM dd, yyyy")).FontSize(9);
                            t.Cell().Background(bg).Padding(5).Text(DisplayName(c.ContributionType)).FontSize(9);
                            t.Cell().Background(bg).Padding(5).AlignRight().Text($"PHP {c.Amount:N2}").FontSize(9);
                        }

                        if (items.Count == 0)
                        {
                            t.Cell().ColumnSpan(4).Padding(14).AlignCenter()
                                .Text("No contributions found.").FontSize(9).FontColor(Colors.Grey.Darken1);
                        }
                    });

                    // Total footer
                    col.Item().BorderTop(1).BorderColor(Colors.Grey.Lighten1).PaddingTop(6).Row(r =>
                    {
                        r.RelativeItem().Text("TOTAL").Bold().FontSize(11);
                        r.ConstantItem(100).AlignRight().Text($"PHP {total:N2}").Bold().FontSize(11);
                    });
                });

                page.Footer().PaddingTop(8).Row(r =>
                {
                    r.RelativeItem().Text("CBS – LAMI Church").FontSize(8).FontColor(Colors.Grey.Darken1);
                    r.ConstantItem(80).AlignRight().Text(x =>
                    {
                        x.Span("Page ").FontSize(8).FontColor(Colors.Grey.Darken1);
                        x.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Darken1);
                        x.Span(" of ").FontSize(8).FontColor(Colors.Grey.Darken1);
                        x.TotalPages().FontSize(8).FontColor(Colors.Grey.Darken1);
                    });
                });
            });
        }).GeneratePdf();
    }

    // ── Monthly expense report ───────────────────────────────────────────

    public static byte[] BuildMonthlyExpenseReport(
        int year, int month,
        IEnumerable<Expense> expenses,
        Func<int, string> getVendorName)
    {
        var items = expenses
            .Where(e => e.ExpenseDate.Year == year && e.ExpenseDate.Month == month)
            .OrderBy(e => e.ExpenseDate)
            .ToList();
        var total = items.Sum(e => e.Amount);
        var monthName = new DateTime(year, month, 1).ToString("MMMM yyyy");
        var byVendor = items
            .GroupBy(e => getVendorName(e.VendorId))
            .Select(g => (Vendor: g.Key, Amount: g.Sum(e => e.Amount)))
            .OrderByDescending(x => x.Amount)
            .ToList();

        return Document.Create(doc =>
        {
            doc.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(s => s.FontSize(10));

                page.Header().Column(col =>
                {
                    col.Item().Row(r =>
                    {
                        r.RelativeItem().Column(c =>
                        {
                            c.Item().Text("CBS – Church Budgeting System").Bold().FontSize(15);
                            c.Item().Text("LAMI Church").FontSize(11).FontColor(Colors.Grey.Darken1);
                        });
                        r.ConstantItem(160).AlignRight().Column(c =>
                        {
                            c.Item().Text("Monthly Expense Report").Bold().FontSize(11);
                            c.Item().Text(monthName).FontSize(11).FontColor("#E65100");
                            c.Item().Text($"Generated: {DateTime.Now:MMM dd, yyyy}").FontSize(9).FontColor(Colors.Grey.Darken1);
                        });
                    });
                    col.Item().PaddingTop(8).BorderBottom(1).BorderColor(Colors.Grey.Lighten1);
                });

                page.Content().PaddingTop(14).Column(col =>
                {
                    // Summary card
                    col.Item().Background("#FFF3E0").Padding(10).Row(r =>
                    {
                        r.RelativeItem().Column(c =>
                        {
                            c.Item().Text("Report Period").FontSize(8).FontColor(Colors.Grey.Darken1);
                            c.Item().Text(monthName).Bold().FontSize(13);
                            c.Item().Text($"{items.Count} expense{(items.Count == 1 ? "" : "s")} recorded").FontSize(9).FontColor(Colors.Grey.Darken1);
                        });
                        r.ConstantItem(130).AlignRight().Column(c =>
                        {
                            c.Item().Text("Total Expenses").FontSize(8).FontColor(Colors.Grey.Darken1);
                            c.Item().Text($"PHP {total:N2}").Bold().FontSize(15).FontColor("#E65100");
                            c.Item().Text($"{byVendor.Count} vendor{(byVendor.Count == 1 ? "" : "s")}").FontSize(9).FontColor(Colors.Grey.Darken1);
                        });
                    });

                    col.Item().PaddingTop(14).Text("Expense Details").Bold().FontSize(12);
                    col.Item().PaddingTop(6).Table(t =>
                    {
                        t.ColumnsDefinition(c =>
                        {
                            c.ConstantColumn(35);
                            c.RelativeColumn(2);
                            c.RelativeColumn(2);
                            c.RelativeColumn(3);
                            c.RelativeColumn(3);
                            c.RelativeColumn(2);
                        });

                        t.Header(h =>
                        {
                            foreach (var (lbl, right) in new[] { ("ID", false), ("Date", false), ("Vendor", false), ("Description", false), ("Type", false), ("Amount", true) })
                            {
                                var cell = h.Cell().Background("#E65100").Padding(5);
                                if (right) cell = cell.AlignRight();
                                cell.Text(lbl).Bold().FontColor(Colors.White).FontSize(9);
                            }
                        });

                        for (int i = 0; i < items.Count; i++)
                        {
                            var e = items[i];
                            var bg = i % 2 == 0 ? Colors.White : Colors.Grey.Lighten5;
                            t.Cell().Background(bg).Padding(5).Text(e.Id.ToString()).FontSize(9);
                            t.Cell().Background(bg).Padding(5).Text(e.ExpenseDate.ToString("MMM dd")).FontSize(9);
                            t.Cell().Background(bg).Padding(5).Text(getVendorName(e.VendorId)).FontSize(9);
                            t.Cell().Background(bg).Padding(5).Text(e.Description).FontSize(9);
                            t.Cell().Background(bg).Padding(5).Text(DisplayName(e.ExpenseType)).FontSize(9);
                            t.Cell().Background(bg).Padding(5).AlignRight().Text($"PHP {e.Amount:N2}").FontSize(9);
                        }

                        if (items.Count == 0)
                        {
                            t.Cell().ColumnSpan(6).Padding(14).AlignCenter()
                                .Text("No expenses recorded for this period.").FontSize(9).FontColor(Colors.Grey.Darken1);
                        }
                    });

                    col.Item().BorderTop(1).BorderColor(Colors.Grey.Lighten1).PaddingTop(6).Row(r =>
                    {
                        r.RelativeItem().Text("TOTAL").Bold().FontSize(11);
                        r.ConstantItem(100).AlignRight().Text($"PHP {total:N2}").Bold().FontSize(11);
                    });

                    // By-vendor breakdown
                    if (byVendor.Any())
                    {
                        col.Item().PaddingTop(16).Text("By Vendor").Bold().FontSize(12);
                        col.Item().PaddingTop(6).Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(4);
                                c.RelativeColumn(2);
                                c.RelativeColumn(2);
                            });

                            t.Header(h =>
                            {
                                h.Cell().Background("#E65100").Padding(5).Text("Vendor").Bold().FontColor(Colors.White).FontSize(9);
                                h.Cell().Background("#E65100").Padding(5).AlignRight().Text("Amount").Bold().FontColor(Colors.White).FontSize(9);
                                h.Cell().Background("#E65100").Padding(5).AlignRight().Text("% of Total").Bold().FontColor(Colors.White).FontSize(9);
                            });

                            for (int i = 0; i < byVendor.Count; i++)
                            {
                                var v = byVendor[i];
                                var bg = i % 2 == 0 ? Colors.White : Colors.Grey.Lighten5;
                                var pct = total > 0 ? v.Amount / total * 100m : 0m;
                                t.Cell().Background(bg).Padding(5).Text(v.Vendor).FontSize(9);
                                t.Cell().Background(bg).Padding(5).AlignRight().Text($"PHP {v.Amount:N2}").FontSize(9);
                                t.Cell().Background(bg).Padding(5).AlignRight().Text($"{pct:N1}%").FontSize(9);
                            }
                        });
                    }
                });

                page.Footer().PaddingTop(8).Row(r =>
                {
                    r.RelativeItem().Text("CBS – LAMI Church").FontSize(8).FontColor(Colors.Grey.Darken1);
                    r.ConstantItem(80).AlignRight().Text(x =>
                    {
                        x.Span("Page ").FontSize(8).FontColor(Colors.Grey.Darken1);
                        x.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Darken1);
                        x.Span(" of ").FontSize(8).FontColor(Colors.Grey.Darken1);
                        x.TotalPages().FontSize(8).FontColor(Colors.Grey.Darken1);
                    });
                });
            });
        }).GeneratePdf();
    }

    // ── Helpers ──────────────────────────────────────────────────────────

    private static string DisplayName(Enum value)
    {
        var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
        var attr = member?.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
        return attr?.Name ?? value.ToString();
    }
}
