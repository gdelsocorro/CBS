using System.ComponentModel.DataAnnotations;
using CBS.Domain.Enums;
using CBS.Domain.Models;

namespace CBS.Shared.ViewModels;

public class MemberViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public Areas Area { get; set; }

    public bool IsActive { get; set; } = true;

    public string Email { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}".Trim();

    public static MemberViewModel From(Member m) => new()
    {
        Id        = m.Id,
        FirstName = m.FirstName,
        LastName  = m.LastName,
        Area      = m.Area,
        IsActive  = m.IsActive,
        Email     = m.Email
    };

    public Member ToNew() => new()
    {
        FirstName = FirstName.Trim(),
        LastName  = LastName.Trim(),
        Area      = Area,
        IsActive  = IsActive,
        Email     = Email.Trim()
    };

    public void ApplyTo(Member m)
    {
        m.FirstName = FirstName.Trim();
        m.LastName  = LastName.Trim();
        m.Area      = Area;
        m.IsActive  = IsActive;
        m.Email     = Email.Trim();
    }
}
