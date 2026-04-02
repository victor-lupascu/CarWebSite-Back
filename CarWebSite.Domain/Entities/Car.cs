using CarWebSite.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarWebSite.Domain.Entities
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        public int Mileage { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public FuelType FuelType { get; set; }
        public TransmissionType Transmission { get; set; }
        public CarCondition Condition { get; set; }

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        public BodyType BodyType { get; set; }
        public Enums.DriveType DriveType { get; set; }
        public ColorCategory? Color { get; set; }
        public NumberOfDoors? Doors { get; set; }

        public int? Seats { get; set; }

        [StringLength(20)]
        public string? EngineSize { get; set; }

        public int? Horsepower { get; set; }

        [StringLength(17)]
        public string? VIN { get; set; }


        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;

        public ICollection<CarImage> Images { get; set; } = new List<CarImage>();
        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}