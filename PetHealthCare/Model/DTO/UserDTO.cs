using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model.DTO;

public class UserDTO
{
    public Guid Id { get; set; }
    public string FullName { get; set; }

    public string? Avatar { get; set; }

    public string Email { get; set; }

    public string? DateOfBirth { get; set; }
    public bool IsUpgrade { get; set; }
    public bool Upgraded { get; set; }
    public DateTime UpgradeDate { get; set; }

    public string? Address { get; set; }

    public UserStatus Status { get; set; }

    public virtual ICollection<PetUserResponseDTO>? Pets { get; set; }
    public string? PhoneNumber { get; set; } = null;

    public virtual ICollection<AppointmentResponseDTO>? Appointments { get; set; }
    public virtual ICollection<MemberUserDTO>? MemberUsers { get; set; }
    public virtual ICollection<NotificatonResponse>? Notifications { get; set; }
}