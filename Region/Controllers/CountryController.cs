using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public IActionResult AddCountry()
        {
            try
            {
                var CountryList = _db.Countries.ToList();
                ViewBag.Country = CountryList;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddCountry(Country country)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (_db.Countries.Where(u => u.CountryName == country.CountryName).Any())
                    {
                        TempData["ErrorMessage"] = "Country is already exists.";
                        return RedirectToAction("AddCountry");
                    }
                    else
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
                }
                return RedirectToAction("AddCountry");
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;
                return View();
            }

        }

        [HttpGet]
        public IActionResult UpdateCountry(int id)
        {
            try
            {
                var detailCountry = _db.Countries.FirstOrDefault(x => x.CountryId == id);
                if (detailCountry != null)
                {
                    return View(detailCountry);
                }
                return RedirectToAction("AddCountry");
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult UpdateCountry(Country country)
        {
            try
            {
                var updateCountry = _db.Countries.FirstOrDefault(x => x.CountryId == country.CountryId);
                if (updateCountry != null)
                {
                    updateCountry.CountryName = country.CountryName;
                    updateCountry.UpdateOn = DateTime.Now;

                    if (_db.Countries.Where(u => u.CountryName == country.CountryName && u.CountryId != country.CountryId).Any())
                    {
                        TempData["ErrorMessage"] = "Country is already exists.";
                        return View();
                    }
                    else
                    {
                        _db.Entry(updateCountry).State = EntityState.Modified;
                        _db.SaveChanges();
                        return RedirectToAction("AddCountry");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult DeleteCountry(int id)
        {
            try
            {
                var deleteCountry = _db.Countries.FirstOrDefault(x => x.CountryId == id);
                if (deleteCountry != null)
                {
                    if (deleteCountry.IsActive == true && deleteCountry.IsDelete == false)
                    {
                        deleteCountry.IsDelete = true;
                        _db.Countries.Remove(deleteCountry);
                        _db.SaveChanges();
                        return RedirectToAction("AddCountry");
                    }
                }
                return RedirectToAction("AddCountry");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
