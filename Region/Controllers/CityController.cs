using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

            ModelState.Remove("CountryId");
            ModelState.Remove("CountryName");
            ModelState.Remove("StateId");
            ModelState.Remove("StateName");

            if (ModelState.IsValid)

            {
                if (_db.Cities.Where(u => u.CityName == city.CityName).Any())
                {
                    TempData["ErrorMessage"] = "City is already exists.";
                    return RedirectToAction("AddCity");
                }
                else
                {
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
            }
            return RedirectToAction("AddCity");
        }

       [HttpGet]
        public IActionResult UpdateCity(int id)
        {
            CountryList();

            var detailCity = _db.Cities.FirstOrDefault(x => x.CityId == id);

            if(detailCity != null)
            {
                List<State> statelist = new List<State>();
                statelist = (from state in _db.States where state.CountryId == detailCity.CountryId select state).ToList();
                ViewBag.stateName = new SelectList(statelist, "StateId", "StateName").OrderBy(a => a.Text);

                return View(detailCity);
            }
            return RedirectToAction("AddCity");
        }

        [HttpPost]
        public IActionResult UpdateCity(City city)
        {
            CountryList();

            var updateCity = _db.Cities.FirstOrDefault(x => x.CityId == city.CityId);
            if (updateCity != null)
            {
                List<State> statelist = new List<State>();
                statelist = (from state in _db.States where state.CountryId == updateCity.CountryId select state).ToList();
                ViewBag.stateName = new SelectList(statelist, "StateId", "StateName").OrderBy(a => a.Text);


                updateCity.CountryId = city.CountryId;
                updateCity.StateId = city.StateId;
                updateCity.CityName = city.CityName;
                updateCity.UpdateOn = DateTime.Now;

                if (_db.Cities.Where(u => u.CityName == city.CityName && u.CityId != city.CityId).Any())
                {
                    TempData["ErrorMessage"] = "City is already exists.";
                    return View();
                }
                else
                {
                    _db.Entry(updateCity).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("AddCity");
                }
            }

            return View();
        }

        public IActionResult DeleteCity(int id)
        {
            var deleteCity = _db.Cities.FirstOrDefault(x => x.CityId == id);
            if (deleteCity != null)
            {
                if (deleteCity.IsActive == true && deleteCity.IsDelete == false)
                {
                    deleteCity.IsDelete = true;
                    _db.Cities.Remove(deleteCity);
                    _db.SaveChanges();
                    return RedirectToAction("AddCity");
                }
            }
            return RedirectToAction("AddCity");
        }


        [NonAction]
        private void CountryList()
        {
            var countryList = _db.Countries.ToList();
            ViewBag.countryList = new SelectList(countryList, "CountryId", "CountryName");
        }

        //[NonAction]
        //private void StateList()
        //{
        //    var stateList = _db.States.ToList();
        //    //var  stateList = _db.States.Where(x => x.CountryId == id).ToList();
        //    ViewBag.StateList = new SelectList(stateList, "StateId","StateName");
           
        //}


        public JsonResult Getstate(int CountryId)
        {
            List<State> statelist = new List<State>();
            statelist = (from state in _db.States where state.CountryId == CountryId select state).ToList();
            statelist.Insert(0, new State { StateId = 0, StateName = "Select State" });
            ViewBag.StateList = new SelectList(statelist, "StateId", "StateName");
            return Json(new SelectList(statelist, "StateId", "StateName"));
        }

    }
}
