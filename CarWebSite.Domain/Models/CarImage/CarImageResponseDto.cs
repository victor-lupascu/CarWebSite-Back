namespace CarWebSite.Domain.Models.CarImage
{
    public class CarImageResponseDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public bool IsCover { get; set; }
        public int CarId { get; set; }
    }
}
