using CarWebSite.Domain.Enums;
using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.CarImage;

namespace CarWebSite.Domain.Models.Car
{
    public class CarResponseDto
    {
        public int Id { get; set; }
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
        public BrandResponseDto Brand { get; set; } = new BrandResponseDto();
        public List<CarImageResponseDto> Images { get; set; } = new List<CarImageResponseDto>();
    }
}