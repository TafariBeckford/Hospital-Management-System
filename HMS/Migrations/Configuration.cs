namespace HMS.Migrations
{
    using HMS.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HMS.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //context.Users.Add(new Models.ApplicationUser { });

           

            var store = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(store);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var user = new ApplicationUser
            {
                Id=Guid.NewGuid().ToString(),
                UserName = "email@email.com",
                Email = "email@email.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Gender = "Male",
                Address = "Address",
                Town = "Town",
                Parish = "Parish",
                DOB = DateTime.Now,
                UserType = ""
            };

            userManager.Create(user, "Password123@#");
            

           roleManager.Create(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Administrator" });
           
           userManager.AddToRole(user.Id, "Administrator");
        }
    }
}
