using Repairshop.Models;
using Repairshop.Services;
using Repairshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repairshop.Controllers
{
    public class PartsController : Controller
    {
        DbAccesPoint db;

        public PartsController(DbAccesPoint db)
        {
            this.db = db;
        }

        // GET: Parts
        public ActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Repairguy"))
            {
                var model = db.GetAllPartsInStorage();
                return View(model);
            }
            return Redirect("Home");
        }

        [Authorize(Roles = "Repairguy,Admin")]
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            PartsEditViewModel editview = new PartsEditViewModel
            {
                amountparts = db.getAmountById(Id)
            };
            return View(editview);
        }

        [Authorize(Roles = "Repairguy,Admin")]
        [HttpPost]
        public ActionResult Edit(PartsEditViewModel editview)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    var part = db.getAmountById(editview.amountparts.Id);
                    part.AmountInStorage = editview.amountparts.AmountInStorage;
                    part.Part.Name = editview.amountparts.Part.Name;
                    part.Part.Brand = editview.amountparts.Part.Brand;
                    part.Part.Type = editview.amountparts.Part.Type;
                    part.Part.Price = editview.amountparts.Part.Price;
                    context.amountParts.AddOrUpdate(part);
                    context.SaveChanges();
                }
            }
            return View();
        }
        [Authorize(Roles = "Repairguy,Admin")]
        public ActionResult Details(int Id)
        {
            var model = db.getAmountById(Id);
            return View(model);
        }
        [Authorize(Roles = "Repairguy,Admin")]
        public ActionResult Delete()
        {
            return View();
        }

    }
}