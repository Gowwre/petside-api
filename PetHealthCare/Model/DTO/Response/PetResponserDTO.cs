using Mapster;
using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model.DTO.Response;

public class PetResponserDTO
{
    // [AdaptMember(nameof(Offerings.Id))]
    [AdaptMember(nameof(Pets.Id))]
    public Guid? PetId { get; set; }
    public string? Name { get; set; }
    public string? Species { get; set; }
    public string? ImagePet { get; set; }
    public DateTime? BirthDate { get; set; }
    public PetStatus? Gender { get; set; }
    public double? Weight { get; set; }
}
