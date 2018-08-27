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
    [Authorize(Roles = "Admin")]
    public class AttractionTypeController : Controller
    {
        // GET: AttractionType
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

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
            var service = new AttractionTypeService(userID);
            var attractionTypes = service.GetAttractionTypes();

            if (!String.IsNullOrEmpty(searchString))
            {
                attractionTypes = attractionTypes.Where(t => t.AttractionTypeName.ToLower().Contains(searchString.ToLower()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    attractionTypes = attractionTypes.OrderByDescending(t => t.AttractionTypeName);
                    break;
                default:
                    attractionTypes = attractionTypes.OrderBy(t => t.AttractionTypeName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(attractionTypes.ToPagedList(pageNumber, pageSize));
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttractionTypeCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateAttractionTypeService();

            if (service.CreateAttractionType(model))
            {
                TempData["SaveResult"] = "Your Attracation Type was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Attraction Type could not be created.");


            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateAttractionTypeService();
            var detail = service.GetAttractionTypeById(id);
            var model =
                new AttractionTypeEdit
                {
                    AttractionTypeID = detail.AttractionTypeID,
                    AttractionTypeName = detail.AttractionTypeName,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AttractionTypeEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.AttractionTypeID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateAttractionTypeService();

            if (service.UpdateAttractionType(model))
            {
                TempData["SaveResult"] = "Your Attraction Type was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Attraction Type could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAttractionTypeService();
            var model = svc.GetAttractionTypeById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateAttractionTypeService();

            service.DeleteAttractionType(id);

            TempData["SaveResult"] = "Your attraction type was deleted";

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var svc = CreateAttractionTypeService();
            var model = svc.GetAttractionTypeById(id);

            return View(model);
        }

        private AttractionTypeService CreateAttractionTypeService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new AttractionTypeService(userID);
            return service;
        }
    }
}
