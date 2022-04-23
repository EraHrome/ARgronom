using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ARgronom.Contexts;
using System.Linq;
using ARgronom.Models;
using System.Security.Claims;
using ARgronom.Models.ViewModels;
using System.Collections.Generic;

namespace ARgronom.Controllers
{
    public class ArController : Controller
    {
        private readonly ILogger<ArController> _logger;
        private readonly Context _context;

        public ArController(ILogger<ArController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userMarkers = _context.Markers.Where(x => x.UserId == userId).ToList();

            var model = new List<ArViewModel>();
            foreach (var userMarker in userMarkers)
            {
                var userPlant = _context.UserPlants.FirstOrDefault(p =>
                    p.Id == int.Parse(userMarker.PlantId));

                var plant = _context.Plants.FirstOrDefault(p =>
                    p.Id == int.Parse(userPlant.PlantId));

                model.Add(new ArViewModel
                {
                    UserMarker = userMarker,
                    Plant = plant
                });
            }

            return View(model);
        }

        public IActionResult AddCoord(string latit, string longit, string userPlantId)
        {
            var userPlant = _context.UserPlants.FirstOrDefault(x => x.Id == int.Parse(userPlantId));
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var oldMarker = _context.Markers.FirstOrDefault(x => x.PlantId == userPlantId && x.UserId == userId);
            if (oldMarker != null)
            {
                _context.Markers.Remove(oldMarker);
                _context.SaveChanges();
            }

            var newMarker = new Marker()
            {
                Latitude = latit,
                Longitude = longit,
                PlantId = userPlantId,
                UserId = userId
            };
            _context.Markers.Add(newMarker);
            _context.SaveChanges();
            return RedirectToAction("MyDetail", "PersonalArea", new { userPlantId = userPlantId });
        }

    }
}
