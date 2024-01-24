using System.ComponentModel.DataAnnotations;
using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model.DTO;

public class PetDto
{
    [Required] public string Name { get; set; }

    [Required] public string Species { get; set; }

    [Required] public string Breed { get; set; }

    [Required] public string BirthDate { get; set; }

    [Required] public int Age { get; set; }

    [Required] public PetStatus Gender { get; set; }

    [Required] public double Weight { get; set; }

    [Required] public Guid OwnerId { get; set; }
}