using PetHealthCare.Model.Abstract;
using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model;

public class Membership : Common
{
    public MembershipStatus Status { get; set; }
    public double Amount { get; set; }
    public virtual ICollection<MemberUser>? MemberUsers { get; set; }
}