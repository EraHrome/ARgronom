using System.ComponentModel.DataAnnotations;
using System;

namespace ARgronom.Models
{
    public class UserPlant
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PlantId { get; set; }
        public string MarkerId { get; set; }
        /// <summary>
        /// Последнее время полива.
        /// </summary>
        public DateTime LastWateringTime { get; set; }

        /// <summary>
        /// Последнее время удобрения.
        /// </summary>
        public DateTime RecentFertilizer { get; set; }

    }
}
