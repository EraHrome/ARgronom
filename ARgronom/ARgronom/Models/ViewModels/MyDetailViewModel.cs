using ARgronom.Models.Weather;

namespace ARgronom.Models.ViewModels
{
    public class MyDetailViewModel
    {
        public UserPlant UserPlant { get; set; }
        public Plants Plant { get; internal set; }
        public WeatherApiResponse Weather { get; set; }
    }
}
