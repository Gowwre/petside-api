﻿using PetHealthCare.Model.Abstract;
using System.Text.Json.Serialization;

namespace PetHealthCare.Model;

public class Providers : Common
{
    public string? ProviderName { get; set; }
    public string? ContactInformation { get; set; }
    public string? ImageProvider { get; set; }
    public string Username { get; set; }
    [JsonIgnore] public byte[] PasswordHash { get; set; } = null!;
    [JsonIgnore] public byte[] PasswordSalt { get; set; } = null!;
    public string? ServiceType { get; set; }
    public string? Availability { get; set; }
    public double? Rating { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public virtual ICollection<OfferProviders>? OfferProviders { get; set; }
    public virtual ICollection<Appointment>? Appointments { get; set; }
}
