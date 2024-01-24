using System.ComponentModel.DataAnnotations;

namespace PetHealthCare.Model.DTO.Request;

public class LoginDto
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }
}