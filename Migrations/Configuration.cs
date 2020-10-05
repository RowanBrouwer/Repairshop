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
        }
    }
}
