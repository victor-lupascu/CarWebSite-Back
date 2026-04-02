using System.ComponentModel.DataAnnotations;

namespace CarWebSite.Domain.Models.CarImage
{
    public class CarImageCreateDto
    {
        [Required]
        public string Url { get; set; } = string.Empty;

        public bool IsCover { get; set; } = false;

        public int CarId { get; set; }
    }
}
