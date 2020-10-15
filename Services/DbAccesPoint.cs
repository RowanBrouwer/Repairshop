using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Repairshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Repairshop.Services
{
    public interface DbAccesPoint
    {
        IEnumerable<RepairOrder> GetRepairOrders();
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerById(int Id);
        RepairOrder GetOrderById(int Id);
        ApplicationUser GetUserById(string Id);
        IEnumerable<RepairOrder> GetRepairOrdersByCustomerId(int Id);
        IEnumerable<RepairOrder> GetRepairOrdersByRepairGuyId(int Id);
        ApplicationUser GetUser(string user);
        ApplicationUser GetUserByName(string username);
        IEnumerable<RepairOrder> GetRepairOrdersByUserName(string username);
        IEnumerable<AmountPartsInStorage> GetAllPartsInStorage();
        AmountPartsInStorage getAmountById(int Id);
        part GetPartInfoByAmountId(int id);
        IEnumerable<RepairGuy> GetRepairGuys();
        IEnumerable<part> GetParts();
        RepairGuy GetRepairGuyByUser(string user);
        Customer GetCustomerByUser(string user);
    }

    public class DbCommands : DbAccesPoint
    {

        public ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<AmountPartsInStorage> GetAllPartsInStorage()
        {
            return db.amountParts.OrderBy(a => a.AmountInStorage).Include("part");
        }

        public AmountPartsInStorage getAmountById(int Id)
        {
            return db.amountParts.FirstOrDefault(r => r.Id == Id);
        }


        public Customer GetCustomerById(int Id)
        {
            return db.customers.FirstOrDefault(r => r.Id == Id);
        }

        public Customer GetCustomerByUser(string user)
        {
            return db.customers.FirstOrDefault(c => c.user.UserName == user);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return db.customers.OrderBy(r => r.user.FirstName);
        }

        public RepairOrder GetOrderById(int Id)
        {
            return db.repairOrders.Where(r => r.Id == Id).Include("parts.PartNeeded").Include("Customer.User").Include("RepairGuy.User").FirstOrDefault();
        }

        public part GetPartInfoByAmountId(int id)
        {
            return db.parts.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<part> GetParts()
        {
            return db.parts.OrderBy(r => r.Price);
        }

        public RepairGuy GetRepairGuyByUser(string user)
        {
            return db.repairGuys.FirstOrDefault(r => r.user.UserName == user);
        }

        public IEnumerable<RepairGuy> GetRepairGuys()
        {
            return db.repairGuys.OrderBy(g => g.Complaints).Include("user");
        }

        public IEnumerable<RepairOrder> GetRepairOrders()
        {
            return db.repairOrders.OrderBy(r => r.status).Include("Customer.User").Include("RepairGuy.User").Include("parts");
        }

        public IEnumerable<RepairOrder> GetRepairOrdersByCustomerId(int Id)
        {
            return db.repairOrders.Where(r => r.customer.Id == Id).Include("parts");
        }

        public IEnumerable<RepairOrder> GetRepairOrdersByRepairGuyId(int Id)
        {
            return db.repairOrders.Where(r => r.repairGuy.Id == Id).Include("parts");
        }

        public IEnumerable<RepairOrder> GetRepairOrdersByUserName(string username)
        {
            return db.repairOrders.Where(c => c.customer.user.UserName == username);
        }

        public ApplicationUser GetUser(string user)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            return userManager.FindById(user);
        }

        public ApplicationUser GetUserById(string Id)
        {
            return db.Users.FirstOrDefault(r => r.Id == Id);
        }

        public ApplicationUser GetUserByName(string username)
        {
            return db.Users.FirstOrDefault(r => r.UserName == username);
        }
    }
}
