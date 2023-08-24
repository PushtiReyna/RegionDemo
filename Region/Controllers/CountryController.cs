using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Region.Models;
using System.Text.Json.Serialization;

namespace Region.Controllers
{
    public class CountryController : Controller
    {
        private readonly RegionDbContext _db;

        public CountryController(RegionDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult AddCountry()
        {
            
            var CountryList = _db.Countries.ToList();
            ViewBag.Country = CountryList;
            return View();

        }
        [HttpPost]
        public IActionResult AddCountry(Country country)
        {
            if (ModelState.IsValid)
            {
                var addCountry = new Country()
                {
                    CountryName = country.CountryName,
                    CreateOn = DateTime.Now,
                    IsActive = true,
                };
                _db.Countries.Add(addCountry);
                _db.SaveChanges();
                return RedirectToAction("AddCountry");
            }
            return View();
        }

        public IActionResult EditCountry(int id)
        {
            var detailCountry = _db.Countries.FirstOrDefault(x => x.CountryId == id);
            if (detailCountry != null)
            {
                ViewBag.Country = detailCountry;
            }
            return RedirectToAction("Index");

            //return ViewBag.Country;
        }

        public IActionResult  UpdateCountry(Country country)
        {
            var  updateCountry = _db.Countries.FirstOrDefault(x => x.CountryId ==  country.CountryId);
            if (updateCountry != null)
            {
                updateCountry.CountryName = country.CountryName;
                updateCountry.UpdateOn = DateTime.Now;

                _db.Entry(updateCountry).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("AddCountry");
            }

            return View();
        }
        //[HttpGet]
        //public JsonResult EditUserJ(int id)
        //{
        //    var user = _db.Countries.Find(id);
        //    if (user != null)
        //    {
        //        return Json(user); 
        //    }
        //    return Json(null); 
        //}
        //public JsonResult EditUserJ(Country country)
        //{
        //    var updateCountry = _db.Countries.FirstOrDefault(x => x.CountryId == country.CountryId);
        //    if (updateCountry != null)
        //    {
        //        updateCountry.CountryName = country.CountryName;
        //        updateCountry.UpdateOn = DateTime.Now;

        //        _db.Entry(updateCountry).State = EntityState.Modified;
        //        _db.SaveChanges();
        //        return Json(country);
        //    }
        //    return Json(null);
        //}

        public IActionResult DeleteCountry(int id)
        {
            var deleteCountry = _db.Countries.FirstOrDefault(x => x.CountryId == id);
            if (deleteCountry != null)
            {
                if(deleteCountry.IsActive == true && deleteCountry.IsDelete == false)
                {
                    deleteCountry.IsDelete = true;
                    _db.Countries.Remove(deleteCountry);
                    _db.SaveChanges();
                    return RedirectToAction("AddCountry");
                }
            }
            return RedirectToAction("AddCountry");
        }

    }
}
