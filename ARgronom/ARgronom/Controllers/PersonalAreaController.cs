using Microsoft.AspNetCore.Mvc;
using ARgronom.Contexts;

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
            return View();
        }

    }
}
