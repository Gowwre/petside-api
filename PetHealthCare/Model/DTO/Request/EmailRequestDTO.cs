namespace PetHealthCare.Model.DTO.Request;

public class EmailRequestDTO
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string From { get; set; }
}
