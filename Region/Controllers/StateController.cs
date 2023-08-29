using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Region.Models;
using System.Diagnostics.Metrics;

namespace Region.Controllers
{
    public class StateController : Controller
    {
        private readonly RegionDbContext _db;

        public StateController(RegionDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult AddState()
        {
            CountryList();
            var ListState = _db.States.ToList();
            ViewBag.state = ListState;

            return View();
        }

        [HttpPost]
        public IActionResult AddState(State state)
        {
            CountryList();
            if (ModelState.IsValid)
            {
                    var StateAdd = new State()
                    {
                        CountryId = state.CountryId,
                        StateName = state.StateName,
                        CreateOn = DateTime.Now,
                        IsActive = true,
                    };
                    _db.States.Add(StateAdd);
                    _db.SaveChanges();
                    return RedirectToAction("AddState");
            }
            return RedirectToAction("AddState");
        }

        [NonAction]
        private void CountryList()
        {
            var countryList = _db.Countries.ToList();
            ViewBag.List = new SelectList(countryList, "CountryId", "CountryName");
        }
    }
}
