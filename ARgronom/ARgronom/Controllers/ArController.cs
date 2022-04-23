using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ARgronom.Contexts;

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
            return View();
        }

    }
}
