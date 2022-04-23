using System.ComponentModel.DataAnnotations;

namespace ARgronom.Models
{
    public class Marker
    {
        [Key]
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PlantId { get; set; }
        public string UserId { get; set; }

    }
}
