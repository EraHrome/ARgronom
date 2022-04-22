using System.ComponentModel.DataAnnotations;
using System;

namespace ARgronom.Models
{
    public class ViewObject
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Id пользователя.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Id растения.
        /// </summary>
        public int PlantId { get; set; }
    }
}
