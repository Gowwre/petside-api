using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetHealthCare.Model.Abstract;

public abstract class Common
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [JsonIgnore]
    [Column("CreateDateTime")]
    public DateTime CreateDateTime { get; set; } = DateTime.Now;
    [JsonIgnore]
    [Column("UpdateDateTime")]
    public DateTime? UpdateDateTime { get; set; } = null;
    [JsonIgnore]
    [Column("DeleteDateTime")]
    public DateTime? DeleteDateTime { get; set; } = null;
}
