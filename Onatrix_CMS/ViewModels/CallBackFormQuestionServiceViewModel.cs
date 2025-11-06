using System.ComponentModel.DataAnnotations;

namespace Onatrix_CMS.ViewModels;

public class CallBackFormQuestionServiceViewModel
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
    public string QuestionEmail { get; init; } = null!;

    [Required(ErrorMessage = "Question is required")]
    [MinLength(6, ErrorMessage = "Question must be at least 6 characters")]
    [Display(Name = "Question")]
    public string Question { get; init; } = null!;
}
