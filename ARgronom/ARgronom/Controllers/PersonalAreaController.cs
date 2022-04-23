using Microsoft.AspNetCore.Mvc;
using ARgronom.Contexts;
using System.Security.Claims;
using System.Linq;
using ARgronom.Models.ViewModels;

namespace ARgronom.Controllers
{
    public class PersonalAreaController : Controller
    {
        private readonly Context _context;
        public PersonalAreaController(Context context)
        {
            _context = context;
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

        public IActionResult MyDetail(string plantId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userPlant = _context.UserPlants.FirstOrDefault(x => x.PlantId == plantId && x.UserId == userId);
            var plant = _context.Plants.FirstOrDefault(x => x.Id.ToString() == userPlant.PlantId);
            var model = new MyDetailViewModel()
            {
                UserPlant = userPlant,
                Plant = plant
            };
            return View(model);
        }

    }
}
