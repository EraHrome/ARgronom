using System.ComponentModel.DataAnnotations;

namespace ARgronom.Models
{
    public class Marker
    {
        [Key]
        public int Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string PlantId { get; set; }
        public string UserId { get; set; }

    }
}
