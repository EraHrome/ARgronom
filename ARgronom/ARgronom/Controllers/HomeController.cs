using ARgronom.Contexts;
using ARgronom.Models;
using ARgronom.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ARgronom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var allCategoriesAndImages = new Dictionary<string, string>();
            foreach (var item in _context.Plants)
            {
                if (!allCategoriesAndImages.ContainsKey(item.Category))
                {
                    allCategoriesAndImages.Add(item.Category, item.ImageName);
                }
            }
            var model = new HomeIndexViewModel()
            {
                AllCategoriesAndImages = allCategoriesAndImages
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
