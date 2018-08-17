using AmusementParkExplorer.Data;
using AmusementParkExplorer.Models;
using AmusementParkExplorer.Services;
using Microsoft.AspNet.Identity;
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
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new AttractionService(userID);
            var model = service.GetAttraction();

            return View(model);
        }

        // GET
        public ActionResult Create()
        {
            ViewBag.ParkID = new SelectList(db.Parks, "ParkID", "ParkName");

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
                    AttractionName = detail.AttractionName,
                    AttractionType = detail.AttractionType,
                    AttractionRating = detail.AttractionRating,
                };
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
