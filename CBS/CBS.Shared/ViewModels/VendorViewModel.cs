using System.ComponentModel.DataAnnotations;
using CBS.Domain.Models;

namespace CBS.Shared.ViewModels;

public class VendorViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vendor name is required")]
    public string Name    { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public static VendorViewModel From(Vendor v) => new()
    {
        Id      = v.Id,
        Name    = v.Name,
        Address = v.Address
    };

    public Vendor ToNew() => new()
    {
        Name      = Name.Trim(),
        Address   = Address.Trim(),
        DateAdded = DateTime.Today
    };

    public void ApplyTo(Vendor v)
    {
        v.Name    = Name.Trim();
        v.Address = Address.Trim();
    }
}
