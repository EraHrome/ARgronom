using Microsoft.AspNetCore.Mvc;
using ARgronom.Contexts;
using System.Security.Claims;
using System.Linq;
using ARgronom.Models.ViewModels;
using System;
using ARgronom.Services;
using ARgronom.Models.Weather;
using System.Threading.Tasks;

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
            var searchPlantsIds = _context.UserPlants
                .Where(x => x.UserId == userId)
                .Select(x => x.PlantId).ToList();
            var plants = _context.Plants.Where(x => searchPlantsIds.Contains(x.Id.ToString())).ToList();
            var model = new PersonalAreaIndexModel() { UserPlants = plants };
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

        public async Task<IActionResult> MyDetail(string plantId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userPlant = _context.UserPlants.FirstOrDefault(x => x.PlantId == plantId && x.UserId == userId);
            var plant = _context.Plants.FirstOrDefault(x => x.Id.ToString() == userPlant.PlantId);

            var marker = _context.Markers.FirstOrDefault(x => x.PlantId == plantId && x.UserId == userId);
            WeatherApiResponse weather = null;
            if(marker != null)
            {
                weather = await _weatherService.GetWeather(marker.Latitude, marker.Longitude);
            }

            var model = new MyDetailViewModel()
            {
                UserPlant = userPlant,
                Plant = plant, 
                Weather = weather
            };
            return View(model);
        }

        public IActionResult UpdateEventTime(string plantId, string eventName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userPlant = _context.UserPlants.FirstOrDefault(x => x.PlantId == plantId && x.UserId == userId);
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

            return RedirectToAction(nameof(MyDetail), new { PlantId = plantId });
        }

        //public IActionResult AddComment(string plantId, string subject, string message)
        //{

        //}

    }
}
