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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            var plant = _context.Plants.FirstOrDefault(p => p.Id == id);
            return View(plant);
        }
    }
}
