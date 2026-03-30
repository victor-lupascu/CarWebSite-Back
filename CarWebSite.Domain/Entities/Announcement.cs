using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarWebSite.Domain.Entities
{
    public class Announcement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;

        public int UserDataId { get; set; }
        public UserData UserData { get; set; } = null!;

        public int CarId { get; set; }
        public Car Car { get; set; } = null!;
    }
}