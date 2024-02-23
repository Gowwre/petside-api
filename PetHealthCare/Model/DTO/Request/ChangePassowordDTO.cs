using System.ComponentModel.DataAnnotations;

namespace PetHealthCare.Model.DTO.Request;

public class ChangePassowordDTO
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }

    [Required] public string NewPassword { get; set; }

    [Required]
    [Compare("NewPassword", ErrorMessage = "NewPassword and Confirm Password do not match.")]
    public string ConfirmPassword { get; set; }
}