using PetHealthCare.Model.Abstract;
using PetHealthCare.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetHealthCare.Model;

[Table("Users", Schema = "dbo")]
public class Users : Common
{
    public string FullName { get; set; }

    public string? Avatar { get; set; }

    [Required][EmailAddress] public string Email { get; set; }

    public string? DateOfBirth { get; set; }
    public long? OtpEmail { get; set; }

    public string? Address { get; set; }

    public UserStatus Status { get; set; }

    [JsonIgnore][Column("PasswordHash")] public byte[] PasswordHash { get; set; } = null!;

    [JsonIgnore][Column("PasswordSalt")] public byte[] PasswordSalt { get; set; } = null!;

    public string? PhoneNumber { get; set; } = null;

    [JsonIgnore] public virtual ICollection<UsersRole>? UsersRoles { get; set; }
    public virtual ICollection<MemberUser>? MemberUsers { get; set; }
    public virtual ICollection<Pets>? Pets { get; set; }
    public virtual ICollection<Payment>? Payments { get; set; }
    public virtual ICollection<Appointment>? Appointments { get; set; }
    public virtual ICollection<Notifications>? Notifications { get; set; }
}