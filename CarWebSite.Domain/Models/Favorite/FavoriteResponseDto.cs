using CarWebSite.Domain.Models.Car;

namespace CarWebSite.Domain.Models.Favorite
{
    public class FavoriteResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public CarResponseDto Car { get; set; } = new CarResponseDto();
        public DateTime CreatedAt { get; set; }
    }
}