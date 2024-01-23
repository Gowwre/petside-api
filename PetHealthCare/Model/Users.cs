using PetHealthCare.Model.Abstract;
using PetHealthCare.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetHealthCare.Model;
[Table("Users", Schema = "dbo")]
public class Users : Common
{
    [Required]
    [StringLength(50), MinLength(1)]
    [Column("FirstName")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50), MinLength(1)]
    [Column("LastName")]
    public string LastName { get; set; }
    public string Avatar { get; set; } // Avatar
    [Required]
    [EmailAddress]
    [Column("Email")]
    public string Email { get; set; }
    public string BirthDay { get; set; }
    public string Address { get; set; }
    //[Column(TypeName = "nvarchar")]
    public UserStatus Status { get; set; }
    [JsonIgnore]
    [Column("PasswordHash")]
    public byte[] PasswordHash { get; set; } = null!;
    [JsonIgnore]
    [Column("PasswordSalt")]
    public byte[] PasswordSalt { get; set; } = null!;

    [Column("PhoneNumber")]
    public string? PhoneNumber { get; set; } = null;
    [JsonIgnore]
    public virtual ICollection<UsersRole>? UsersRoles { get; set; }
    public virtual ICollection<Pets>? Pets { get; set; }
}
