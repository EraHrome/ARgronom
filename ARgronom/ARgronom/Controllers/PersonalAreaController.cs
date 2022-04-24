using Microsoft.AspNetCore.Mvc;
using ARgronom.Contexts;
using System.Security.Claims;
using System.Linq;
using ARgronom.Models.ViewModels;
using System;
using ARgronom.Services;
using ARgronom.Models.Weather;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ARgronom.Controllers
{
    public class PersonalAreaController : Controller
    {
        private readonly Context _context;
        private readonly WeatherService _weatherService;

        public PersonalAreaController(Context context, WeatherService weatherService)
        {
            _context = context;
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var plants = _context.Plants.ToList();
            var userPlants = _context.UserPlants.Where(x => x.UserId == userId);

            var model = new List<PersonalAreaIndexModel>();
            foreach (var userPlant in userPlants)
            {
                var plant = plants.FirstOrDefault(p => p.Id == int.Parse(userPlant.PlantId));

                model.Add(new PersonalAreaIndexModel()
                {
                    Plant = plant,
                    UserPlant = userPlant
                });
            }

            return View(model);
        }

        public IActionResult AddPlant(string plantId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.UserPlants.Add(new Models.UserPlant()
            {
                PlantId = plantId,
                UserId = userId,
            });
            _context.SaveChanges();
            return RedirectToAction("Index", "PersonalArea");
        }

        public async Task<IActionResult> MyDetail(string userPlantId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userPlant = _context.UserPlants.FirstOrDefault(x => x.Id == int.Parse(userPlantId) && x.UserId == userId);
            var plant = _context.Plants.FirstOrDefault(x => x.Id.ToString() == userPlant.PlantId);

            var marker = _context.Markers.FirstOrDefault(x => x.PlantId == userPlantId && x.UserId == userId);
            WeatherApiResponse weather = null;
            if(marker != null)
            {
                weather = await _weatherService.GetWeather(marker.Latitude, marker.Longitude);
            }

            var model = new MyDetailViewModel()
            {
                UserPlant = userPlant,
                Plant = plant, 
                Weather = weather,
                Marker = marker
            };

            return View(model);
        }

        public IActionResult UpdateEventTime(string userPlantId, string eventName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userPlant = _context.UserPlants.FirstOrDefault(x => x.Id == int.Parse(userPlantId) && x.UserId == userId);
            switch (eventName)
            {
                case "watering":
                    userPlant.LastWateringTime = DateTime.Now;
                    break;
                case "fertilizing":
                    userPlant.RecentFertilizer = DateTime.Now;
                    break;
            }
            _context.Update(userPlant);
            _context.SaveChanges();

            return RedirectToAction(nameof(MyDetail), new { UserPlantId = userPlantId });
        }

        public async Task<IActionResult> Calendar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var plants = _context.Plants.ToList();
            var userPlants = _context.UserPlants.Where(p => p.UserId == userId);

            var model = new List<CalendarViewModel>();
            foreach (var userPlant in userPlants)
            {
                var plant = plants.FirstOrDefault(p => p.Id == int.Parse(userPlant.PlantId));
                var wateringDate = userPlant.LastWateringTime.AddDays(plant.WateringFrequency);
                var fertilizingDate = userPlant.RecentFertilizer.AddDays(plant.FertilizerFrequency);

                if(wateringDate >= DateTime.Now)
                {
                    model.Add(new CalendarViewModel()
                    {
                        Title = $"Полить {plant.Category} {plant.Title}",
                        DateStart = wateringDate.ToString("s")
                    });
                }
                if(fertilizingDate >= DateTime.Now)
                {
                    model.Add(new CalendarViewModel()
                    {
                        Title = $"Удобрить {plant.Category} {plant.Title}",
                        DateStart = fertilizingDate.ToString("s")
                    });
                }
            }

            var userMark = _context.Markers.FirstOrDefault(m => m.UserId == userId);
            if(userMark != null)
            {
                var weather = await _weatherService.GetWeather(userMark.Latitude, userMark.Longitude);
                int daysCounter = 1;
                model.Add(new CalendarViewModel()
                {
                    Title = $"Погода - {weather.current.weather.First().description} ",
                    DateStart = DateTime.Now.ToString("yyyy-MM-dd")
                });
                foreach (var daily in weather.daily.Take(7))
                {
                    model.Add(new CalendarViewModel()
                    {
                        Title = $"Погода -  {daily.weather.First().description} ",
                        DateStart = DateTime.Now.AddDays(daysCounter).ToString("yyyy-MM-dd")
                    });
                    daysCounter++;
                }
            }

            return View(model);
        }

        //public IActionResult AddComment(string plantId, string subject, string message)
        //{

        //}

    }
}
