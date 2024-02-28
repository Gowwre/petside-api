namespace PetHealthCare.Model.DTO.Request;

public class NotificationRequest
{
    public string NameMedicine { get; set; }
    public DateTime DateRemind { get; set; }
    public int TimeRemind { get; set; }
    public string Content { get; set; }
    public int Dusage { get; set; }
    public int Age { get; set; }
}
