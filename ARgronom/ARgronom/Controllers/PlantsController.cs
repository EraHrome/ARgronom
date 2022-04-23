using ARgronom.Contexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARgronom.Controllers
{
    public class PlantsController : Controller
    {
        private readonly Context _context;

        public PlantsController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(string category)
        {
            var plants = _context.Plants.AsEnumerable();
            if (!String.IsNullOrEmpty(category))
            {
                plants = plants.Where(x => x.Category == category);
            }
            return View(plants);
        }

        public IActionResult Detail(int id)
        {
            var plant = _context.Plants.FirstOrDefault(p => p.Id == id);
            return View(plant);
        }
    }
}
