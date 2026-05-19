using CBS.Domain.Enums;
using CBS.Domain.Models;

namespace CBS.Shared.ViewModels;

public class ContributionViewModel
{
    public int       Id               { get; set; }
    public int       MemberId         { get; set; }
    public DateTime? Date             { get; set; } = DateTime.Today;
    public decimal   Amount           { get; set; }
    public FundTypes ContributionType { get; set; }

    public static ContributionViewModel From(Fund f) => new()
    {
        Id               = f.Id,
        MemberId         = f.MemberId,
        Date             = f.ContributionDate,
        Amount           = f.Amount,
        ContributionType = f.ContributionType
    };

    public Fund ToNew() => new()
    {
        MemberId         = MemberId,
        ContributionDate = Date ?? DateTime.Today,
        Amount           = Amount,
        ContributionType = ContributionType,
        Month            = (Months)(Date ?? DateTime.Today).Month
    };

    public void ApplyTo(Fund f)
    {
        f.MemberId         = MemberId;
        f.ContributionDate = Date ?? DateTime.Today;
        f.Amount           = Amount;
        f.ContributionType = ContributionType;
        f.Month            = (Months)(Date ?? DateTime.Today).Month;
    }
}
