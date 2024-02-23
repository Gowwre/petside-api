namespace PetHealthCare.Model;

public class MemberUser
{
    public Guid Id { get; set; }
    public DateTime CreateDay { get; set; }
    public DateTime ExpiredDay { get; set; }
    public double? TotalAmount { get; set; }
    public Guid? MembershipId { get; set; }
    public virtual Membership? Membership { get; set; }
    public Guid? UsersId { get; set; }
    public virtual Users? Users { get; set; }
}