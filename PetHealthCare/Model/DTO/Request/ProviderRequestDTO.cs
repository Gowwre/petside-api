namespace PetHealthCare.Model.DTO.Request;

public class ProviderRequestDTO
{
    public string? ProviderName { get; set; }
    public string? ContactInformation { get; set; }
    public string? ServiceType { get; set; }
    public string? Availability { get; set; }
    public double? Rating { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
}
