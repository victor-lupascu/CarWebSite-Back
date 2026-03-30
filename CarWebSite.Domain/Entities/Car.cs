using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarWebSite.Domain.Enums;
using Microsoft.VisualBasic.FileIO;

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

        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;

        public ICollection<CarImage> Images { get; set; } = new List<CarImage>();
        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}