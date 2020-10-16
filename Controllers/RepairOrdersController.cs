using Autofac.Core.Lifetime;
using Microsoft.AspNet.Identity;
using Repairshop.HelperClasses;
using Repairshop.Models;
using Repairshop.Services;
using Repairshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Odbc;
using System.Deployment.Internal;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;

namespace Repairshop.Controllers
{
    public class RepairOrdersController : Controller
    {
        DbAccesPoint db;

        public RepairOrdersController(DbAccesPoint db)
        {
            this.db = db;
        }

        [Authorize]
        public ActionResult Index()
        {
            IEnumerable<RepairOrder> model;
            if (User.IsInRole("Admin") || User.IsInRole("Repairguy"))
            {
                model = db.GetRepairOrders();
            }
            else if (User.IsInRole("Customer"))
            {
                model = db.GetRepairOrdersByUserName(User.Identity.Name);
            }
            else
            {
                model = db.GetRepairOrdersByUserName(User.Identity.Name);
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Details(int Id)
        {
            var model = db.GetOrderById(Id);
            return View(model);
        }

        [HttpGet]
        public ActionResult DetailsEdit(int Id)
        {
            if ((User.IsInRole("Admin") || User.IsInRole("Repairguy")))
            {
                DetailsEditViewModel editview = new DetailsEditViewModel
                {
                    order = db.GetOrderById(Id)
                };
                return View(editview);
            }
            return View();
        }

        [HttpPost]
        public ActionResult DetailsEdit(DetailsEditViewModel viewmodel, int Id)
        {
            if ((User.IsInRole("Admin") || User.IsInRole("Repairguy")))
            {
                if (viewmodel != null)
                {
                    if (ModelState.IsValid)
                    {
                        using (var context = new ApplicationDbContext())
                        {
                            var model = db.GetOrderById(Id);

                            model.parts = viewmodel.order.parts;
                            model.repairGuy = viewmodel.order.repairGuy;
                            model.customer = viewmodel.order.customer;
                            model.Description = viewmodel.order.Description;
                            model.status = viewmodel.order.status;
                            model.StartDate = viewmodel.order.StartDate;
                            model.EndDate = viewmodel.order.EndDate;

                            context.repairOrders.AddOrUpdate(model);
                            context.SaveChanges();

                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreateOrder()
        {
            IEnumerable<RepairGuy> reps = db.GetRepairGuys();
            IEnumerable<AmountPartsInStorage> storage = db.GetAllPartsInStorage();
            IEnumerable<part> parts = db.GetParts();
            ViewBag.reps = reps;
            ViewBag.storage = storage;
            ViewBag.parts = parts;
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(DetailsEditViewModel viewmodel)
        {
            using (var context = new ApplicationDbContext())
            {
                PartsNeeded needed = new PartsNeeded();
                var currentuser = User.Identity.Name;


                // Here is where a customer is made if an user doesnt have a customer linked to it already//
                if (context.customers.Where(c => c.user.UserName == currentuser)== null)
                {
                    Customer AdminCustomer = new Customer();
                    AdminCustomer.user = context.Users.FirstOrDefault(c => c.UserName == currentuser);
                    context.customers.AddOrUpdate(AdminCustomer);
                }

                

                if (User.IsInRole("Customer"))
                {
                    // here the needed part with amount is set //
                    needed.PartNeeded.Id = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id).Id;
                    needed.PartNeeded.Name = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id).Name;
                    needed.PartNeeded.Brand = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id).Brand;
                    needed.PartNeeded.Type = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id).Type;
                    needed.PartNeeded.Price = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id).Price;
                    needed.AmountNeeded = viewmodel.order.parts.AmountNeeded;
                    needed.inStorage.AmountInStorage = context.amountParts.FirstOrDefault(p => p.Part.Id == needed.PartNeeded.Id).AmountInStorage;
                    needed.inStorage.Part = needed.PartNeeded;
                    //added part and amount that is needed //
                    context.partsNeeded.AddOrUpdate(needed);

                    // filling in the rest //
                    viewmodel.order.parts = needed;
                    viewmodel.order.customer = db.GetCustomerByUser(currentuser);
                    viewmodel.order.EndDate = viewmodel.order.StartDate.AddDays(7);
                    viewmodel.order.status = Status.Awaiting;
                    viewmodel.order.StartDate = viewmodel.order.StartDate;
                    viewmodel.order.EndDate = viewmodel.order.StartDate.AddDays(7);
                    viewmodel.order.Description = viewmodel.order.Description;

                    // sh*ts not working //
                    if (ModelState.IsValid)
                    {
                        context.repairOrders.AddOrUpdate(viewmodel.order);
                        context.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    needed.PartNeeded = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id);
                    needed.PartNeeded.Name = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id).Name;
                    needed.PartNeeded.Brand = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id).Brand;
                    needed.PartNeeded.Type = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id).Type;
                    needed.PartNeeded.Price = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id).Price;

                    needed.AmountNeeded = viewmodel.order.parts.AmountNeeded;

                    needed.inStorage = context.amountParts.FirstOrDefault(p => p.Part.Id == needed.PartNeeded.Id);
                    needed.inStorage.AmountInStorage = context.amountParts.FirstOrDefault(p => p.Part.Id == needed.PartNeeded.Id).AmountInStorage;
                    needed.inStorage.Part = needed.PartNeeded;

                    viewmodel.order.parts = needed;

                    viewmodel.order.status = Status.Awaiting;
                    viewmodel.order.StartDate = viewmodel.order.StartDate;
                    viewmodel.order.EndDate = viewmodel.order.StartDate.AddDays(7);

                    viewmodel.order.Description = viewmodel.order.Description;
                    viewmodel.order.customer = viewmodel.order.customer;

                    // Again, sh*ts not working //
                    if (ModelState.IsValid)
                    {
                        context.repairOrders.AddOrUpdate(viewmodel.order);
                        context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }

                return View();
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int Id)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel
            {
                order = db.GetOrderById(Id)
            };
            return View(deleteViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel DeleteView)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    var model = db.GetOrderById(DeleteView.order.Id);
                    model.customer = DeleteView.order.customer;
                    model.repairGuy = DeleteView.order.repairGuy;
                    model.StartDate = DeleteView.order.StartDate;
                    model.status = DeleteView.order.status;
                    model.EndDate = DeleteView.order.EndDate;
                    model.Description = DeleteView.order.Description;
                    context.repairOrders.Remove(model);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}