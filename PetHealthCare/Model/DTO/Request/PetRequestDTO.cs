using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model.DTO.Request;

public class PetRequestDTO
{
    public string Name { get; set; }
    public string Species { get; set; }
    public string? ImagePet { get; set; }
    public DateTime BirthDate { get; set; }
    public PetStatus Gender { get; set; }
    public double Weight { get; set; }
}
