using CarWebSite.Domain.Enums;

namespace CarWebSite.Domain.Models.Announcement
{
    public class AnnouncementCreateDto
    {
        // Announcement fields
        public string Title { get; set; } = string.Empty;
        public bool Negotiable { get; set; } = false;
        public bool ShowPhone { get; set; } = true;

        // Car fields
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }
        public FuelType FuelType { get; set; }
        public TransmissionType Transmission { get; set; }
        public CarCondition Condition { get; set; }
        public string Description { get; set; } = string.Empty;
        public BodyType BodyType { get; set; }
        public string? Color { get; set; }
        public int? Doors { get; set; }
        public int? Seats { get; set; }
        public string? EngineSize { get; set; }
        public int? Horsepower { get; set; }
        public string? VIN { get; set; }
        public int BrandId { get; set; }
    }
}
