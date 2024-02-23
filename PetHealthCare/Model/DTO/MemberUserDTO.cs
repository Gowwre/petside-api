namespace PetHealthCare.Model.DTO;

public class MemberUserDTO
{
    public Guid Id { get; set; }
    public DateTime CreateDay { get; set; }
    public DateTime ExpiredDay { get; set; }
    public double? TotalAmount { get; set; }
    public Guid? MembershipId { get; set; }
    public virtual MembershipDTO? Membership { get; set; }
}