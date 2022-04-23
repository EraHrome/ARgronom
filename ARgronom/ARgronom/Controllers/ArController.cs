using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ARgronom.Contexts;
using System.Linq;
using ARgronom.Models;
using System.Security.Claims;
using ARgronom.Models.ViewModels;

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

        public IActionResult Index(string plantId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userMarkers = _context.Markers.Where(x => x.UserId == userId).ToList();
            var model = new ArViewModel()
            {
                Markers = userMarkers,
                PlantId = plantId
            };
            return View(model);
        }

        public IActionResult AddCoord(string latit, string longit, string plantId)
        {
            var plant = _context.UserPlants.First(x => x.PlantId == plantId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var oldMarker = _context.Markers.FirstOrDefault(x => x.PlantId == plantId && x.UserId == userId);
            if (oldMarker != null)
            {
                _context.Markers.Remove(oldMarker);
                _context.SaveChanges();
            }

            var newMarker = new Marker()
            {
                Latitude = latit,
                Longitude = longit,
                PlantId = plantId,
                UserId = userId
            };
            _context.Markers.Add(newMarker);
            _context.SaveChanges();
            return RedirectToAction("MyDetail", "PersonalArea", new { plantId = plantId });
        }

    }
}
