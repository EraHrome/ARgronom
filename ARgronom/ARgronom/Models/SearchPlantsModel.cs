using System.ComponentModel.DataAnnotations;

namespace ARgronom.Models
{
    public class SearchPlantsModel
    {

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Поисковый запрос
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public string UserId { get; set; }

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
