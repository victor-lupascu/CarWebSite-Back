using CarWebSite.Domain.Enums;
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

        public ListingStatus Status { get; set; } = ListingStatus.Active;

        public bool Negotiable { get; set; } = false;

        public bool ShowPhone { get; set; } = true;

        public int Views { get; set; } = 0;

        public int Inquiries { get; set; } = 0;

        public DateTime PublishedAt { get; set; } 


        public int UserDataId { get; set; }

        [ForeignKey(nameof(UserDataId))]
        public UserData UserData { get; set; } = null!;


        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;
    }
}