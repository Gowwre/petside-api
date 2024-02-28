using PetHealthCare.Model.Abstract;

namespace PetHealthCare.Model;

public class Notifications : Common
{
    public string NameMedicine { get; set; }
    public DateTime DateRemind { get; set; }
    public int TimeRemind { get; set; }
    public string Content { get; set; }
    public int Dusage { get; set; }
    public int Age { get; set; }
    public Guid? PetsId { get; set; }
    public virtual Pets? Pets { get; set; }
    public Guid? UsersId { get; set; }
    public virtual Users? Users { get; set; }
}
