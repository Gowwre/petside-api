﻿using PetHealthCare.Model.Enums;

namespace PetHealthCare.Model.DTO.Response;

public class NotificationPetResponseDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Species { get; set; }
    public string? ImagePet { get; set; }
    public DateTime? BirthDate { get; set; }
    public PetStatus? Gender { get; set; }
    public double? Weight { get; set; }
    public double? Height { get; set; }
    public string? IdentifyingFeatures { get; set; }
}
