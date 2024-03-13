using PetHealthCare.Model.DTO.Response;

namespace PetHealthCare.Model.DTO;

public class NotificationDTO
{
    public string NameMedicine { get; set; }
    public DateTime DateRemind { get; set; }
    public int TimeRemind { get; set; }
    public string Content { get; set; }
    public int Dusage { get; set; }
    public int Age { get; set; }
    public virtual NotificationPetResponseDTO? Pets { get; set; }
    public virtual NotificationUserResponseDTO? Users { get; set; }
}
