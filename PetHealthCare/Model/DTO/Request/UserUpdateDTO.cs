namespace PetHealthCare.Model.DTO.Request;

public class UserUpdateDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Avatar { get; set; }
    //public string ImageUser { get; set; }
    public string BirthDay { get; set; }
    public string Address { get; set; }
    public string? PhoneNumber { get; set; }
}
