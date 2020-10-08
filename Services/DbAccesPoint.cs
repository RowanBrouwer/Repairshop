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
    }

    public class DbCommands : DbAccesPoint
    {

        public ApplicationDbContext db = new ApplicationDbContext();
        
        public Customer GetCustomerById(int Id)
        {
            return db.customers.FirstOrDefault(r => r.Id == Id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return db.customers.OrderBy(r => r.user.FirstName);
        }

        public RepairOrder GetOrderById(int Id)
        {
            return db.repairOrders.FirstOrDefault(r => r.Id == Id);
        }

        public IEnumerable<RepairOrder> GetRepairOrders()
        {
            return db.repairOrders.OrderBy(r => r.status);
        }

        public IEnumerable<RepairOrder> GetRepairOrdersByCustomerId(int Id)
        {
            return db.repairOrders.Where(r => r.customer.Id == Id);
        }

        public IEnumerable<RepairOrder> GetRepairOrdersByRepairGuyId(int Id)
        {
            return db.repairOrders.Where(r => r.repairGuy.Id == Id);
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
