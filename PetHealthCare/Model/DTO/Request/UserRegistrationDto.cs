using System.ComponentModel.DataAnnotations;

namespace PetHealthCare.Model.DTO.Request;

public class UserRegistrationDto
{
    [Required] public string FullName { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Password and Repeat Password do not match.")]
    public string RepeatPassword { get; set; }

    [RegularExpression(@"(09|03|07|08|05)([0-9]{8})\b", ErrorMessage = "Invalid Phone Number")]
    public string? PhoneNumber { get; set; }
}