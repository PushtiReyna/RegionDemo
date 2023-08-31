using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Region.Models;

namespace Region.Controllers
{
    public class CityController : Controller
    {
        private readonly RegionDbContext _db;
        public CityController(RegionDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult AddCity()
        {
            CountryList();

            List<City> cityList = new List<City>();
            City city = new City();
            var CityList = _db.Cities.ToList();

            foreach (var item in CityList)
            {
                var countryName = _db.Countries.FirstOrDefault(x => x.CountryId == item.CountryId);
                var stateName = _db.States.FirstOrDefault(x => x.StateId == item.StateId);
                city = new City();
                city.CityName = item.CityName;
                city.CityId = item.CityId;
               
                if (countryName != null)
                {
                    city.CountryName = countryName.CountryName;
                }
                if(stateName != null)
                {
                    city.StateName = stateName.StateName;
                }

                cityList.Add(city);
            }
            ViewBag.city = cityList;
         
            return View();
        }

        [HttpPost]
        public IActionResult AddCity(City city)
        {
            CountryList();
            
            var CityAdd = new City()
            {
                CountryId = city.CountryId,
                StateId = city.StateId,
                CityName = city.CityName,
                
                CreateOn = DateTime.Now,
                IsActive = true,
            };
            _db.Cities.Add(CityAdd);
            _db.SaveChanges();


            return RedirectToAction("AddCity");
            
        }






        [NonAction]
        private void CountryList()
        {
            var countryList = _db.Countries.ToList();
            ViewBag.List = new SelectList(countryList, "CountryId", "CountryName");
        }

        //[NonAction]
        //private void StateList()
        //{
        //    var stateList = _db.States.ToList();
        //    //var  stateList = _db.States.Where(x => x.CountryId == id).ToList();
        //    ViewBag.StateList = new SelectList(stateList, "StateId","StateName");
           
        //}


        //private IActionResult StateList(int? id)
        //{
        //    //var stateList = _db.States.ToList();
        //    var stateList = _db.States.Where(x => x.CountryId == id).ToList();
        //    ViewBag.StateList = new SelectList(stateList, "StateId", "StateName");
        //    return PartialView("Displaystate");
        //}


        public JsonResult Getstate(int CountryId)
        {
            List<State> statelist = new List<State>();
            statelist = (from state in _db.States where state.CountryId == CountryId select state).ToList();
            statelist.Insert(0, new State { StateId = 0, StateName = "Select State" });
            return Json(new SelectList(statelist, "StateId", "StateName"));
        }

    }
}
