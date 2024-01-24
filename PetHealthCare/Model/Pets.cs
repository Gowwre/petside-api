using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PetHealthCare.Model.Abstract;
using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model;

[Table("Pets", Schema = "dbo")]
public class Pets : Common
{
    public string Name { get; set; }
    public string Species { get; set; }
    public string Breed { get; set; }
    public string BirthDate { get; set; }
    public int Age { get; set; }
    public PetStatus Gender { get; set; }
    public double Weight { get; set; }

    [Column("UsersId")] public Guid UsersId { get; set; }

    [JsonIgnore] public virtual Users Users { get; set; }
}