using System.ComponentModel.DataAnnotations;

namespace ARgronom.Models
{
    public class Plants
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageName { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Где растет (локация).
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// Тип почвы.
        /// </summary>
        public string SoilType { get; set; }

        /// <summary>
        /// Удобрение.
        /// </summary>
        public string Fertilizer { get; set; }

        /// <summary>
        /// Как часто удобрять.
        /// </summary>
        public int FertilizerFrequency { get; set; }

        /// <summary>
        /// Минимальное расстояние до след саженца.
        /// </summary>
        public int MinToNextPlant { get; set; }

        /// <summary>
        /// Минимальное расстояние до след грядки.
        /// </summary>
        public int MinToNextGardenBed { get; set; }

        /// <summary>
        /// Нужно ли подвязывать.
        /// </summary>
        public bool NeedToTie { get; set; }

        /// <summary>
        /// Частота полива в днях.
        /// </summary>
        public int WateringFrequency { get; set; }

        /// <summary>
        /// За сколько дней в среднем вырастает.
        /// </summary>
        public int StandardGrowthTime { get; set; }

    }
}
