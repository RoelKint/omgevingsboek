namespace Models.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.MVC_Models;
    using Models.OmgevingsBoek_Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.MVC_Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Models.MVC_Models.ApplicationDbContext context)
        {

            #region ROLES

            string roleSuperAdmin = "SuperAdministrator";
            string roleAdmin = "Administrator";
            string roleNormalUser = "User";

            IdentityResult roleResult;

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(roleAdmin))
                roleResult = roleManager.Create(new IdentityRole(roleAdmin));
            if (!roleManager.RoleExists(roleNormalUser))
                roleResult = roleManager.Create(new IdentityRole(roleNormalUser));
            if (!roleManager.RoleExists(roleSuperAdmin))
                roleResult = roleManager.Create(new IdentityRole(roleSuperAdmin));

            #endregion

            #region USERS


            // TODO: Updaten naar echte data

            if (!context.Users.Any(u => u.Email.Equals("testSuperAdmin@howest.be")))
            {
                var store = new UserStore<Models.MVC_Models.ApplicationUser>(context);
                var manager = new UserManager<Models.MVC_Models.ApplicationUser>(store);
                var user = new Models.MVC_Models.ApplicationUser()
                {
                    Naam = "test",
                    Voornaam = "test",
                    Email = "test@howest.be",
                    UserName = "testSuperAdmin@howest.be",
                    Deleted = false

                };
                manager.Create(user, "paswoord");
                manager.AddToRole(user.Id, roleSuperAdmin);
            }

            if (!context.Users.Any(u => u.Email.Equals("testAdmin@howest.be")))
            {
                var store = new UserStore<Models.MVC_Models.ApplicationUser>(context);
                var manager = new UserManager<Models.MVC_Models.ApplicationUser>(store);
                var user = new Models.MVC_Models.ApplicationUser()
                {
                    Naam = "test",
                    Voornaam = "test",
                    Email = "test@howest.be",
                    UserName = "testAdmin@howest.be",
                    Deleted = false

                };
                manager.Create(user, "paswoord");
                manager.AddToRole(user.Id, roleAdmin);
            }

            if (!context.Users.Any(u => u.Email.Equals("testUser@howest.be")))
            {
                var store = new UserStore<Models.MVC_Models.ApplicationUser>(context);
                var manager = new UserManager<Models.MVC_Models.ApplicationUser>(store);
                var user = new Models.MVC_Models.ApplicationUser()
                {
                    Naam = "test",
                    Voornaam = "test",
                    Email = "test@howest.be",
                    UserName = "testUser@howest.be",
                    Deleted = false

                };
                manager.Create(user, "paswoord");
                manager.AddToRole(user.Id, roleNormalUser);
            }

            #endregion

            #region DATA

            //Reële demodata

            if (context.Poi.Count() == 0)
            {
                Poi poi = new Poi()
                {
                    Naam = "Gentpoort",
                    EigenaarId = context.Users.First().Id,
                    Straat = "Gentpoortstraat",
                    Nummer = "48",
                    Gemeente = "Brugge",
                    Email = "musea@brugge.be",
                    Telefoon = "+32 50 44 87 43",
                    Postcode = 8000,

                };


            }



            #endregion

        }
    }
}
