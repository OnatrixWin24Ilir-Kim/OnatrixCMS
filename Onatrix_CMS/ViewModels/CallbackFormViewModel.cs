using System.ComponentModel.DataAnnotations;

namespace Onatrix_CMS.ViewModels;

public class CallbackFormViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name")]
    public string Name { get; init; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [Display(Name = "Email")]
    [RegularExpression(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "Please enter a valid email address"
    )]
    [StringLength(100, ErrorMessage = "Email must be less than 100 characters")]
    public string Email { get; init; } = null!;

    [Required(ErrorMessage = "Phone is required")]
    [Phone(ErrorMessage = "Please enter a valid phone number")]
    [Display(Name = "Phone")]
    [StringLength(10, ErrorMessage = "Phone must be less than 10 characters")]
    public string Phone { get; init; } = null!;

    [Required(ErrorMessage = "Please select an option")]
    [Display(Name = "Please select an option")]
    public string SelectedOption { get; init; } = null!;
}
