using System.ComponentModel.DataAnnotations;

namespace ARgronom.Models
{
    public class IdentityRole
    {

        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public Role Role { get; set; }

    }
}
