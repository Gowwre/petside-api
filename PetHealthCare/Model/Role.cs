using PetHealthCare.Model.Abstract;
using PetHealthCare.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetHealthCare.Model;
[Table("Role", Schema = "dbo")]
public class Role : Common
{
    [Required]
    //[Column("RoleName", TypeName = "nvarchar")]
    public RoleName RoleName { get; set; }
    [JsonIgnore]
    public virtual ICollection<UsersRole> UsersRoles { get; set; }
}
