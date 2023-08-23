using Microsoft.AspNetCore.Mvc;
using Region.Models;

namespace Region.Controllers
{
    public class CountryController : Controller
    {
        private readonly RegionDbContext _db;

        public CountryController(RegionDbContext db)
        {
            _db = db;
        }

        public IActionResult AddCountry()
        {
            return View();
        }
    }
}
