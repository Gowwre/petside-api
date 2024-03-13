using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model.DTO.Response;

public class NotificationUserResponseDTO
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
}
