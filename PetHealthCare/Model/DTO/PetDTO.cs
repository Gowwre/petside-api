using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model.DTO;

public class PetDTO
{
    public string Name { get; set; }
    public string Species { get; set; }
    public string Breed { get; set; }
    public string BirthDate { get; set; }
    public int Age { get; set; }
    public PetStatus Gender { get; set; }
    public double Weight { get; set; }
}