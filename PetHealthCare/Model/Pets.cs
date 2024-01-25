using PetHealthCare.Model.Abstract;
using PetHealthCare.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetHealthCare.Model;
[Table("Pets", Schema = "dbo")]
public class Pets : Common
{
    public string? Name { get; set; }
    public string? Species { get; set; }
    public string? ImagePet { get; set; }
    public DateTime? BirthDate { get; set; }
    public PetStatus? Gender { get; set; }
    public double? Weight { get; set; }
    [Column("UsersId")]
    public Guid UsersId { get; set; }
    [JsonIgnore]
    public virtual Users Users { get; set; }
    public Guid? AppointmentId { get; set; }
    public virtual Appointment? Appointment { get; set; }
}
