using System.Text.Json.Serialization;

namespace PetHealthCare.Model;

public class UsersRole
{
    public Guid UsersId { get; set; }
    public Guid RoleId { get; set; }
    [JsonIgnore]
    public virtual Users? Users { get; set; }
    [JsonIgnore]
    public virtual Role? Role { get; set; }
}
