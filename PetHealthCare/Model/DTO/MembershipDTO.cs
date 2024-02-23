using System.Text.Json.Serialization;
using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model.DTO;

public class MembershipDTO
{
    public Guid Id { get; set; }

    [JsonIgnore] public MembershipStatus Status { get; set; }

    public string StatusName => Status.ToString();
    public double Amount { get; set; }
}