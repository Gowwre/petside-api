using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PetHealthCare.Model.Abstract;
using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model;

[Table("Users", Schema = "dbo")]
public class Users : Common
{
    [Required]
    [StringLength(100)]
    [MinLength(1)]
    [Column("Fullname")]
    public string FullName { get; set; }

    public string? Avatar { get; set; } // Avatar

    [Required]
    [EmailAddress]
    [Column("Email")]
    public string Email { get; set; }

    public string? DateOfBirth { get; set; }

    public string? Address { get; set; }

    //[Column(TypeName = "nvarchar")]
    public UserStatus Status { get; set; }
    [JsonIgnore] [Column("PasswordHash")] public byte[] PasswordHash { get; set; } = null!;
    [JsonIgnore] [Column("PasswordSalt")] public byte[] PasswordSalt { get; set; } = null!;

    [Column("PhoneNumber")] public string? PhoneNumber { get; set; } = null;
    [JsonIgnore] public virtual ICollection<UsersRole>? UsersRoles { get; set; }
    public virtual ICollection<Pets>? Pets { get; set; }
}