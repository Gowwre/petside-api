using System.ComponentModel.DataAnnotations;

namespace PetHealthCare.Model.DTO.Request;

public class UserDTO
{
    public string FullName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Password and Repeat Password do not match.")]
    public string RepeatPassword { get; set; }
    public string Avarta { get; set; }
    public string BirthDay { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
}
