using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarWebSite.Domain.Entities
{
    public class CarImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; } = string.Empty;

        public bool IsCover { get; set; } = false;

        public int CarId { get; set; }
        public Car Car { get; set; } = null!;
    }
}