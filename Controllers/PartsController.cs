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
    [Authorize(Roles = "Repairguy,Admin")]
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
            var model = db.GetAllPartsInStorage();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            PartsEditViewModel editview = new PartsEditViewModel
            {
                amountparts = db.getAmountById(Id)
            };
            return View(editview);
        }

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

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

    }
}