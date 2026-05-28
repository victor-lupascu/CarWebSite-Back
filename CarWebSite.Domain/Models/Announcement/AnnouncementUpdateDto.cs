using CarWebSite.Domain.Enums;
using CarWebSite.Domain.Models.CarImage;

namespace CarWebSite.Domain.Models.Announcement
{
    public class AnnouncementUpdateDto
    {
        // Announcement fields
        public string? Title { get; set; }
        public bool? Negotiable { get; set; }
        public bool? ShowPhone { get; set; }
        public ListingStatus? Status { get; set; }

        // Car fields
        public string? Model { get; set; }
        public int? Year { get; set; }
        public int? Mileage { get; set; }
        public decimal? Price { get; set; }
        public FuelType? FuelType { get; set; }
        public TransmissionType? Transmission { get; set; }
        public CarCondition? Condition { get; set; }
        public string? Description { get; set; }
        public BodyType? BodyType { get; set; }
        public ColorCategory? Color { get; set; }
        public NumberOfDoors? Doors { get; set; }
        public int? Seats { get; set; }
        public string? EngineSize { get; set; }
        public int? Horsepower { get; set; }
        public string? VIN { get; set; }
        public int? BrandId { get; set; }
        public List<CarImageCreateDto>? Images { get; set; }
    }
}
