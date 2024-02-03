using PetHealthCare.Model.Enums;
using System.Text.Json.Serialization;

namespace PetHealthCare.Model.DTO;

public class MembershipDTO
{
    public Guid Id { get; set; }
    [JsonIgnore]
    public MembershipStatus Status { get; set; }
    public string StatusName { get { return Status.ToString(); } }
    public double Amount { get; set; }

}
