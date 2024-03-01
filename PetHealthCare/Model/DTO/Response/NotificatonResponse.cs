namespace PetHealthCare.Model.DTO.Response;

public class NotificatonResponse
{
    public Guid Id { get; set; }
    public string NameMedicine { get; set; }
    public DateTime DateRemind { get; set; }
    public int TimeRemind { get; set; }
    public string Content { get; set; }
    public int Dusage { get; set; }
    public int Age { get; set; }
}
