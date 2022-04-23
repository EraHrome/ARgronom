using System.ComponentModel.DataAnnotations;

namespace ARgronom.Models
{
    public class UserPlant
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PlantId { get; set; }
        public string MarkerId { get; set; }

    }
}
