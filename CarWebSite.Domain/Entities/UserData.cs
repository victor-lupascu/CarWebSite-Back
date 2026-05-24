using CarWebSite.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarWebSite.Domain.Entities
{
    public class UserData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(200, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        public UserRole Role { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegisteredOn { get; set; }

        // Rate Limiting fields
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockedUntil { get; set; }

        // Navigation properties
        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
        public ICollection<FavoriteData> Favorites { get; set; } = new List<FavoriteData>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}