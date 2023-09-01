using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            try
            {
                CountryList();

                //var ListState = _db.States.ToList();
                // ViewBag.state = ListState;

                List<State> stateList = new List<State>();
                State state = new State();
                var StateList = _db.States.ToList();

                foreach (var item in StateList)
                {
                    var countryName = _db.Countries.FirstOrDefault(x => x.CountryId == Convert.ToInt64(item.CountryId));
                    state = new State();
                    state.StateName = item.StateName;
                    state.StateId = item.StateId;
                    if (countryName != null)
                    {
                        state.CountryName = countryName.CountryName;
                    }
                    stateList.Add(state);
                }
                ViewBag.state = stateList;
                return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult AddState(State state)
        {
            try
            {
                CountryList();

                // ModelState.Remove("CountryId");
                ModelState.Remove("CountryName");
                ModelState.Remove("Country");
                if (ModelState.IsValid)
                {
                    if (_db.States.Where(u => u.StateName == state.StateName).Any())
                    {
                        TempData["ErrorMessage"] = "State is already exists.";
                        return RedirectToAction("AddState");
                    }
                    else
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
                }
                return RedirectToAction("AddState");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult UpdateState(int id)
        {
            try
            {
                CountryList();

                var detailState = _db.States.FirstOrDefault(x => x.StateId == id);
                if (detailState != null)
                {

                    return View(detailState);
                }
                return RedirectToAction("AddState");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdateState(State state)
        {
            try
            {
                CountryList();
                var updateState = _db.States.FirstOrDefault(x => x.StateId == state.StateId);
                if (updateState != null)
                {
                    updateState.CountryId = state.CountryId;
                    updateState.StateName = state.StateName;
                    updateState.UpdateOn = DateTime.Now;
                    if (_db.States.Where(u => u.StateName == state.StateName && u.StateId != state.StateId).Any())
                    {
                        TempData["ErrorMessage"] = "state is already exists.";
                        return View();
                    }
                    else
                    {
                        _db.Entry(updateState).State = EntityState.Modified;
                        _db.SaveChanges();
                        return RedirectToAction("AddState");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult DeleteState(int id)
        {
            try
            {
                var deleteState = _db.States.FirstOrDefault(x => x.StateId == id);
                if (deleteState != null)
                {
                    if (deleteState.IsActive == true && deleteState.IsDelete == false)
                    {
                        deleteState.IsDelete = true;
                        _db.States.Remove(deleteState);
                        _db.SaveChanges();
                        return RedirectToAction("AddState");
                    }
                }
                return RedirectToAction("AddState");
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [NonAction]
        private void CountryList()
        {
            var countryList = _db.Countries.ToList();
            ViewBag.countryList = new SelectList(countryList, "CountryId", "CountryName");
        }
    }
}
