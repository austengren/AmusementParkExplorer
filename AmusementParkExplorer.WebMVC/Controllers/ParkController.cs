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
    [Authorize(Roles = "Admin")]
    public class ParkController : Controller
    {
        // GET: Park
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ParkService(userID);
            var model = service.GetParks();

            return View(model);
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParkCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateParkService();

            if (service.CreatePark(model))
            {
                TempData["SaveResult"] = "Your Park was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Your park could not be added.");

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateParkService();
            var detail = service.GetParkById(id);
            var model =
                new ParkEdit
                {
                    ParkID = detail.ParkID,
                    ParkName = detail.ParkName,
                    City = detail.City,
                    State = detail.State,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ParkEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ParkID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateParkService();

            if (service.UpdatePark(model))
            {
                TempData["SaveResult"] = "Your Park was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Park could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateParkService();
            var model = svc.GetParkById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateParkService();

            service.DeletePark(id);

            TempData["SaveResult"] = "Your park was deleted";

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var svc = CreateParkService();
            var model = svc.GetParkById(id);

            return View(model);
        }

        private ParkService CreateParkService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ParkService(userID);
            return service;
        }
    }
}
