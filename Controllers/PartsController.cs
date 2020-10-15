using Repairshop.Models;
using Repairshop.Services;
using Repairshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repairshop.HelperClasses;

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
        public ActionResult Edit(PartsEditViewModel viewModel, int Id)
        {
            if (viewModel != null)
            {
                var clone = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                clone.NumberFormat.NumberDecimalSeparator = ",";
                clone.NumberFormat.NumberGroupSeparator = ".";
                string s = viewModel.amountparts.Part.Price.ToString();
                double d = double.Parse(s, clone);
                viewModel.amountparts.Part.Price = d;
                if (ModelState.IsValid)
                {
                    var user = User;
                    bool admin = false;
                    bool accessed = false;
                    string switchcase = "Parts";
                    DbAccesPoint idb = db;
                    if (ModelState.IsValid)
                    {
                        SaveClass.SaveChoice(viewModel, accessed, Id, switchcase, idb, user, admin);
                        return RedirectToAction("Index");
                    }
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

        [HttpGet]
        public ActionResult Create()
        {
            var reps = db
            return View();
        }
        [HttpPost]
        public ActionResult Create(PartsEditViewModel viewModel)
        {
            if (viewModel != null)
            {
                var clone = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                clone.NumberFormat.NumberDecimalSeparator = ",";
                clone.NumberFormat.NumberGroupSeparator = ".";
                string s = viewModel.amountparts.Part.Price.ToString();
                double d = double.Parse(s, clone);
                viewModel.amountparts.Part.Price = d;

                if (ModelState.IsValid)
                {
                    var user = User;
                    bool admin = false;
                    bool accessed = true;
                    int Id = 0;
                    string switchcase = "Parts";
                    DbAccesPoint idb = db;
                    if (ModelState.IsValid)
                    {
                        SaveClass.SaveChoice(viewModel, accessed, Id, switchcase, idb, user, admin);
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
    }
}