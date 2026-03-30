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
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(200,MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [StringLength(9)]
        public string? PhoneNumber { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        public UserRole Role { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RegisteredOn { get; set; }


    }
}
