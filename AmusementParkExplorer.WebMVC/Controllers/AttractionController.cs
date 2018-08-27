using AmusementParkExplorer.Data;
using AmusementParkExplorer.Models;
using AmusementParkExplorer.Services;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmusementParkExplorer.WebMVC.Controllers
{
    [Authorize]
    public class AttractionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attraction
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CitySortParm = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "";
            ViewBag.StateSortParm = String.IsNullOrEmpty(sortOrder) ? "state_desc" : "";
            ViewBag.AttractionNameSortParm = String.IsNullOrEmpty(sortOrder) ? "attractionName_desc" : "";
            ViewBag.AttractionTypeNameSortParm = String.IsNullOrEmpty(sortOrder) ? "attractionTypeName_desc" : "";
            ViewBag.AttractionRatingSortParm = String.IsNullOrEmpty(sortOrder) ? "attractionRating_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new AttractionService(userID);
            var attractions = service.GetAttractions();

            if (!String.IsNullOrEmpty(searchString))
            {
                attractions = attractions.Where(a => a.ParkName.ToLower().Contains(searchString.ToLower())
                                 || a.City.ToLower().Contains(searchString.ToLower())
                                 || a.State.ToLower().Contains(searchString.ToLower())
                                 || a.AttractionName.ToLower().Contains(searchString.ToLower())
                                 || a.AttractionTypeName.ToLower().Contains(searchString.ToLower())
                                 || a.AttractionRating.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    attractions = attractions.OrderByDescending(a => a.ParkName);
                    break;
                case "city_desc":
                    attractions = attractions.OrderBy(a => a.City);
                    break;
                case "state_desc":
                    attractions = attractions.OrderBy(a => a.State);
                    break;
                case "attractionName_desc":
                    attractions = attractions.OrderBy(a => a.AttractionName);
                    break;
                case "attractionTypeName_desc":
                    attractions = attractions.OrderBy(a => a.AttractionTypeName);
                    break;
                case "attractionRating_desc":
                    attractions = attractions.OrderBy(a => a.AttractionRating);
                    break;
                default:
                    attractions = attractions.OrderBy(a => a.ParkName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(attractions.ToPagedList(pageNumber, pageSize));
        }

        // GET
        public ActionResult Create()
        {
            ViewBag.ParkID = new SelectList(db.Parks, "ParkID", "ParkName");
            ViewBag.AttractionTypeID = new SelectList(db.AttractionTypes, "AttractionTypeID", "AttractionTypeName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttractionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateAttractionService();

            if (service.CreateAttraction(model))
            {
                TempData["SaveResult"] = "Your Attraction was created.";
                return RedirectToAction("Index");
            };

            ViewBag.ParkID = new SelectList(db.Parks, "ParkID", "ParkName");
            ViewBag.AttractionTypeID = new SelectList(db.AttractionTypes, "AttractionTypeID", "AttractionTypeName");

            ModelState.AddModelError("", "Attraction could not be created.");

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateAttractionService();
            var detail = service.GetAttractionById(id);
            var model =
                new AttractionEdit
                {
                    AttractionID = detail.AttractionID,
                    ParkID = detail.ParkID,
                    AttractionName = detail.AttractionName,
                    AttractionTypeID = detail.AttractionTypeID,
                    AttractionRating = detail.AttractionRating,
                };

            ViewBag.ParkID = new SelectList(db.Parks, "ParkID", "ParkName");
            ViewBag.AttractionTypeID = new SelectList(db.AttractionTypes, "AttractionTypeID", "AttractionTypeName");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AttractionEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.AttractionID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateAttractionService();

            if (service.UpdateAttraction(model))
            {
                TempData["SaveResult"] = "Your Attraction was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Attraction could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAttractionService();
            var model = svc.GetAttractionById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateAttractionService();

            service.DeleteAttraction(id);

            TempData["SaveResult"] = "Your Attraction was deleted";

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var svc = CreateAttractionService();
            var model = svc.GetAttractionById(id);

            return View(model);
        }

        private AttractionService CreateAttractionService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new AttractionService(userID);
            return service;
        }
    }
}
