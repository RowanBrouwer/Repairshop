namespace Repairshop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Repairshop.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Deployment.Internal;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    internal sealed class Configuration : DbMigrationsConfiguration<Repairshop.Models.ApplicationDbContext>
    {
        public Configuration()
        {

        }

        protected override void Seed(Repairshop.Models.ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(RoleNames.ROLE_ADMIN))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_ADMIN));
            }
            if (!roleManager.RoleExists(RoleNames.ROLE_CUSTOMER))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_CUSTOMER));
            }
            if (!roleManager.RoleExists(RoleNames.ROLE_REPAIRGUY))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_REPAIRGUY));
            }

            if (userManager.FindByEmail("user1@localhost") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "user1";
                user.Email = "user1@localhost";
                user.FirstName = "Nancy";
                user.LastName = "Davolio";
                RepairGuy repairGuy = new RepairGuy();
                repairGuy.user = user;
                context.repairGuys.Add(repairGuy);

                IdentityResult result = userManager.CreateAsync
                (user, "!Admin123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (userManager.FindByEmail("user2@localhost") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "user2";
                user.Email = "user2@localhost";
                user.FirstName = "Bob";
                user.LastName = "Builder";
                Customer customer = new Customer();
                customer.user = user;
                context.customers.Add(customer);

                IdentityResult result = userManager.CreateAsync
                (user, "!Cus123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                }
            }

            if (userManager.FindByEmail("user3@localhost") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "user3";
                user.Email = "user3@localhost";
                user.FirstName = "Rowan";
                user.LastName = "Brouwer";
                RepairGuy repairGuy = new RepairGuy();
                repairGuy.user = user;
                context.repairGuys.Add(repairGuy);

                IdentityResult result = userManager.CreateAsync
                (user, "!Rep123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Repairguy");
                }
            }
        }
    }
}
