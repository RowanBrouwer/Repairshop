﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Repairshop.Models;
using Repairshop.Services;
using Repairshop.ViewModels;

namespace Repairshop.Controllers
{
    public class HomeController : Controller
    {
        DbAccesPoint db;

        public HomeController(DbAccesPoint db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit()
        {
            EditViewModel editViewModel = new EditViewModel
            {
                user = db.GetUserByName(User.Identity.Name)
            };

            return View(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel editmodel)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(roleStore);

                    var userStore = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(userStore);

                    var model = db.GetUserByName(User.Identity.Name);
                    model.FirstName = editmodel.user.FirstName;
                    model.LastName = editmodel.user.LastName;
                    model.StreetName = editmodel.user.StreetName;
                    model.City = editmodel.user.City;
                    model.PostCode = editmodel.user.PostCode;

                    context.Users.AddOrUpdate(model);
                    context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(editmodel);
        }
    }
}