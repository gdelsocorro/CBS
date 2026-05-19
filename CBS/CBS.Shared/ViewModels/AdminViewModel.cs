using System.ComponentModel.DataAnnotations;
using CBS.Domain.Models;

namespace CBS.Shared.ViewModels;

public class AdminViewModel
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName       { get; set; } = string.Empty;

    public string LastName        { get; set; } = string.Empty;

    [Required(ErrorMessage = "Username is required")]
    public string Username        { get; set; } = string.Empty;

    [Required, MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password        { get; set; } = string.Empty;

    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public Admin ToNew() => new()
    {
        FirstName    = FirstName.Trim(),
        LastName     = LastName.Trim(),
        Username     = Username.Trim(),
        PasswordHash = Password,
        IsActive     = true
    };
}
