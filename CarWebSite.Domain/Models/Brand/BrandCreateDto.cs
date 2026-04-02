using System.ComponentModel.DataAnnotations;

namespace CarWebSite.Domain.Models.Brand
{
    public class BrandCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
