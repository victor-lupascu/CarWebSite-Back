using CarWebSite.Domain.Enums;

namespace CarWebSite.Domain.Models.Announcement
{
    public class AnnouncementResponseDto
    {
        // Announcement fields
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ListingStatus Status { get; set; }
        public bool Negotiable { get; set; }
        public bool ShowPhone { get; set; }
        public int Views { get; set; }
        public int Inquiries { get; set; }
        public DateTime PublishedAt { get; set; }

        // Owner
        public int UserId { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string? OwnerPhone { get; set; }

        // Car fields
        public int CarId { get; set; }
        public string BrandName { get; set; } = string.Empty;
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
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
