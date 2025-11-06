using System.ComponentModel.DataAnnotations;

namespace Onatrix_CMS.ViewModels;

public class CallBackFormEmailViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [Display(Name = "Email")]
    [RegularExpression(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "Please enter a valid email address"
    )]
    [StringLength(100, ErrorMessage = "Email must be less than 100 characters")]
    public string SubscriptionEmail { get; init; } = null!;
}
