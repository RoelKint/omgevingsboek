namespace Models.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.MVC_Models;
    using Models.OmgevingsBoek_Models;
    using System;
    using System.Collections.Generic;
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
            /*
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
            */
            #endregion

            #region DATA

            Boek boek10 = new Boek()
            {
                Naam = "Roel <3 oude kalende mannen",
                EigenaarId = context.Users.FirstOrDefault().Id,
                DeelLijst = context.Users.ToList(),
                Afbeelding = "https://farm9.staticflickr.com/8885/17827205793_7473869f00.jpg"
            };
            context.Boeken.Add(boek10);
            context.SaveChanges();

            //Reële demodata
            /*
            Activiteit activiteit9 = new Activiteit()
            {
                PoiId = context.Poi.FirstOrDefault().ID,
                Naam = "Een Activiteit",
                Boeken = new List<Boek>(){
                        context.Boeken.FirstOrDefault()
                    },
                DeelLijst = context.Boeken.FirstOrDefault().DeelLijst,
                DitactischeToelichting = "Project: Maak een miniatuurmolen.",
                Uitleg = "Overbrengingen: tandwielen en riemoverbrenging Binnenin de molen zijn zowel tandwieloverbrengingen, als een riemoverbrenging te zien.U kunt de leerlingen laten experimenteren met verschillende overbrengingen. Hiervoor kunt u gebruik maken van verschillende LEGO® sets rond overbrengingen.",
                Prijs = 5,
                MinLeeftijd = 9,
                MaxLeeftijd = 10,
                MinDuur = 50,
                MaxDuur = 50,
                Eigenaar = context.Users.First(),
                

            };
            context.Activiteiten.Add(activiteit9);
            context.SaveChanges();

            Activiteit activiteit10 = new Activiteit()
            {
                PoiId = context.Poi.FirstOrDefault().ID,
                Naam = "Zo een activiteit zeg",
                Boeken = new List<Boek>(){
                        context.Boeken.FirstOrDefault()
                    },
                DeelLijst = context.Boeken.FirstOrDefault().DeelLijst,
                DitactischeToelichting = "Project: Maak een miniatuurmolen.",
                Uitleg = "Overbrengingen: tandwielen en riemoverbrenging Binnenin de molen zijn zowel tandwieloverbrengingen, als een riemoverbrenging te zien.U kunt de leerlingen laten experimenteren met verschillende overbrengingen. Hiervoor kunt u gebruik maken van verschillende LEGO® sets rond overbrengingen.",
                Prijs = 5,
                MinLeeftijd = 9,
                MaxLeeftijd = 10,
                MinDuur = 50,
                MaxDuur = 50,
                Eigenaar = context.Users.Where(i => i.UserName == "testUser@howest.be").FirstOrDefault()


            };
            context.Activiteiten.Add(activiteit10);
            context.SaveChanges();

            Activiteit activiteit4 = new Activiteit()
            {
                PoiId = context.Poi.FirstOrDefault().ID,
                Naam = "Nog een andere activiteit",
                Boeken = new List<Boek>(){
                        context.Boeken.FirstOrDefault()
                    },
                DeelLijst = context.Boeken.FirstOrDefault().DeelLijst,
                DitactischeToelichting = "Project: Maak een miniatuurmolen.",
                Uitleg = "Overbrengingen: tandwielen en riemoverbrenging Binnenin de molen zijn zowel tandwieloverbrengingen, als een riemoverbrenging te zien.U kunt de leerlingen laten experimenteren met verschillende overbrengingen. Hiervoor kunt u gebruik maken van verschillende LEGO® sets rond overbrengingen.",
                Prijs = 5,
                MinLeeftijd = 9,
                MaxLeeftijd = 10,
                MinDuur = 50,
                MaxDuur = 50,
                Eigenaar = context.Users.First(),


            };
            context.Activiteiten.Add(activiteit4);
            context.SaveChanges();

            Activiteit activiteit3 = new Activiteit()
            {
                PoiId = context.Poi.FirstOrDefault().ID,
                Naam = "Activiteit met een zeer lange naam zoals deze hier zo is",
                Boeken = new List<Boek>(){
                        context.Boeken.FirstOrDefault()
                    },
                DeelLijst = context.Boeken.FirstOrDefault().DeelLijst,
                DitactischeToelichting = "Project: Maak een miniatuurmolen.",
                Uitleg = "Overbrengingen: tandwielen en riemoverbrenging Binnenin de molen zijn zowel tandwieloverbrengingen, als een riemoverbrenging te zien.U kunt de leerlingen laten experimenteren met verschillende overbrengingen. Hiervoor kunt u gebruik maken van verschillende LEGO® sets rond overbrengingen.",
                Prijs = 5,
                MinLeeftijd = 9,
                MaxLeeftijd = 10,
                MinDuur = 50,
                MaxDuur = 50,
                Eigenaar = context.Users.Where(i => i.UserName == "testUser@howest.be").FirstOrDefault()


            };
            context.Activiteiten.Add(activiteit3);
            context.SaveChanges();


            Activiteit activiteit5 = new Activiteit()
            {
                PoiId = context.Poi.FirstOrDefault().ID,
                Naam = "1 activiteit",
                Boeken = new List<Boek>(){
                        context.Boeken.FirstOrDefault()
                    },
                DeelLijst = context.Boeken.FirstOrDefault().DeelLijst,
                DitactischeToelichting = "Project: Maak een miniatuurmolen.",
                Uitleg = "Overbrengingen: tandwielen en riemoverbrenging Binnenin de molen zijn zowel tandwieloverbrengingen, als een riemoverbrenging te zien.U kunt de leerlingen laten experimenteren met verschillende overbrengingen. Hiervoor kunt u gebruik maken van verschillende LEGO® sets rond overbrengingen.",
                Prijs = 5,
                MinLeeftijd = 9,
                MaxLeeftijd = 10,
                MinDuur = 50,
                MaxDuur = 50,
                Eigenaar = context.Users.First(),
                

            };
            context.Activiteiten.Add(activiteit5);
            context.SaveChanges();

            Activiteit activiteit6 = new Activiteit()
            {
                PoiId = context.Poi.FirstOrDefault().ID,
                Naam = "Activiteit test",
                Boeken = new List<Boek>(){
                        context.Boeken.FirstOrDefault()
                    },
                DeelLijst = context.Boeken.FirstOrDefault().DeelLijst,
                DitactischeToelichting = "Project: Maak een miniatuurmolen.",
                Uitleg = "Overbrengingen: tandwielen en riemoverbrenging Binnenin de molen zijn zowel tandwieloverbrengingen, als een riemoverbrenging te zien.U kunt de leerlingen laten experimenteren met verschillende overbrengingen. Hiervoor kunt u gebruik maken van verschillende LEGO® sets rond overbrengingen.",
                Prijs = 5,
                MinLeeftijd = 9,
                MaxLeeftijd = 10,
                MinDuur = 50,
                MaxDuur = 50,
                Eigenaar = context.Users.Where(i => i.UserName == "testUser@howest.be").FirstOrDefault()


            };
            context.Activiteiten.Add(activiteit6);
            context.SaveChanges();

            Activiteit activiteit7 = new Activiteit()
            {
                PoiId = context.Poi.FirstOrDefault().ID,
                Naam = "Meer activiteiten",
                Boeken = new List<Boek>(){
                        context.Boeken.FirstOrDefault()
                    },
                DeelLijst = context.Boeken.FirstOrDefault().DeelLijst,
                DitactischeToelichting = "Project: Maak een miniatuurmolen.",
                Uitleg = "Overbrengingen: tandwielen en riemoverbrenging Binnenin de molen zijn zowel tandwieloverbrengingen, als een riemoverbrenging te zien.U kunt de leerlingen laten experimenteren met verschillende overbrengingen. Hiervoor kunt u gebruik maken van verschillende LEGO® sets rond overbrengingen.",
                Prijs = 5,
                MinLeeftijd = 9,
                MaxLeeftijd = 10,
                MinDuur = 50,
                MaxDuur = 50,
                Eigenaar = context.Users.First(),


            };
            context.Activiteiten.Add(activiteit7);
            context.SaveChanges();

            Activiteit activiteit8 = new Activiteit()
            {
                PoiId = context.Poi.FirstOrDefault().ID,
                Naam = "Roels magische activiteit",
                Boeken = new List<Boek>(){
                        context.Boeken.FirstOrDefault()
                    },
                DeelLijst = context.Boeken.FirstOrDefault().DeelLijst,
                DitactischeToelichting = "Project: Maak een miniatuurmolen.",
                Uitleg = "Overbrengingen: tandwielen en riemoverbrenging Binnenin de molen zijn zowel tandwieloverbrengingen, als een riemoverbrenging te zien.U kunt de leerlingen laten experimenteren met verschillende overbrengingen. Hiervoor kunt u gebruik maken van verschillende LEGO® sets rond overbrengingen.",
                Prijs = 5,
                MinLeeftijd = 9,
                MaxLeeftijd = 10,
                MinDuur = 50,
                MaxDuur = 50,
                Eigenaar = context.Users.Where(i => i.UserName == "testUser@howest.be").FirstOrDefault()


            };
            context.Activiteiten.Add(activiteit8);
            context.SaveChanges();
             */

            if (context.Boeken.Select(i => i).Where(i => i.Naam == "1e graad") == null)
            {
                Boek boek4 = new Boek()
                {
                    Naam = "1ste graad",
                    EigenaarId = context.Users.FirstOrDefault().Id,
                    DeelLijst = context.Users.ToList(),
                    Afbeelding = "https://farm9.staticflickr.com/8885/17827205793_7473869f00.jpg"
                };
                Boek boek5 = new Boek()
                {
                    Naam = "Kleuterklas",
                    EigenaarId = context.Users.FirstOrDefault().Id,
                    DeelLijst = context.Users.ToList(),
                    Afbeelding = "https://farm9.staticflickr.com/8890/18227981769_5d800e8e52.jpg"
                };
                Boek boek6 = new Boek()
                {
                    Naam = "Extras",
                    EigenaarId = context.Users.Select(i => i).Where(i => i.UserName == "testAdmin@howest.be").FirstOrDefault().Id,
                    DeelLijst = context.Users.ToList(),
                    Afbeelding = "https://farm9.staticflickr.com/8838/18421446816_a8f819c2f3.jpg"
                };
                context.Boeken.Add(boek4);
                context.Boeken.Add(boek5);
                context.Boeken.Add(boek6);
                context.SaveChanges();

            }

            if (context.Poi.Count() == 0)
            {
                List<string> users = new List<string>();
                
                foreach(ApplicationUser user in context.Users){
                    users.Add(user.Id);
                }

                Boek boek1 = new Boek(){
                    Naam = "3de graad",
                    EigenaarId = context.Users.First().Id,
                    DeelLijst = context.Users.ToList(),
                    Afbeelding = "https://farm1.staticflickr.com/512/18415814341_f03e9495bb.jpg"
                };
                Boek boek2 = new Boek(){
                    Naam = "2de graad",
                    EigenaarId = context.Users.First().Id,
                    DeelLijst = new List<ApplicationUser>(){
                        context.Users.First()
                    },
                    Afbeelding = "https://farm1.staticflickr.com/486/18414141365_da47d30153.jpg"
                };
                Boek boek3 = new Boek()
                {
                    Naam = "gedeeld boek",
                    EigenaarId = context.Users.Select(i =>i).Where(i => i.UserName == "testAdmin@howest.be").FirstOrDefault().Id,
                    DeelLijst = context.Users.ToList(),
                    Afbeelding = "https://farm1.staticflickr.com/545/18447813515_08826f57da.jpg"
                };
                context.Boeken.Add(boek1);
                context.SaveChanges();
                context.Boeken.Add(boek2);
                context.SaveChanges();
                context.Boeken.Add(boek3);
                context.SaveChanges();

                List<Tag> tags = new List<Tag>(){
                    new Tag(){Naam = "Museum"},
                    new Tag(){Naam = "geschiedenis"},
                    new Tag(){Naam = "Brugge"},
                    new Tag(){Naam = "panorama"},
                    new Tag(){Naam = "Molen"}
                    
                };
                List<Tag> tagsActiviteit = new List<Tag>(){
                    new Tag(){Naam = "Museum"},
                    new Tag(){Naam = "panorama"},
                    new Tag(){Naam = "geschiedenis"},
                    new Tag(){Naam = "kruisboog"},
                    new Tag(){Naam = "wapens"},
                    new Tag(){Naam = "Brugge"}
                    
                };
                List<Tag> tagsActiviteit2 = new List<Tag>(){
                    new Tag(){Naam = "Overbrengingen"},
                    new Tag(){Naam = "Tandwielen"},
                    new Tag(){Naam = "Molen"}
                    
                };
                List<Benodigdheid> benodigdheden = new List<Benodigdheid>(){
                    new Benodigdheid(){Naam = "Papier"},
                    new Benodigdheid(){Naam = "Pen"}
                };

                foreach (Tag tag in tags)
                {
                    context.Tags.AddOrUpdate(tag);
                }
                context.SaveChanges();

                foreach (Tag tag in tagsActiviteit)
                {
                    context.Tags.AddOrUpdate(tag);
                }
                context.SaveChanges();

                foreach (Tag tag in tagsActiviteit2)
                {
                    context.Tags.AddOrUpdate(tag);
                }
                context.SaveChanges();

                foreach(Benodigdheid b in benodigdheden){
                    context.Benodigdheden.AddOrUpdate(b);
                }
                context.SaveChanges();


                Poi poi1 = new Poi()
                {
                    Naam = "Gentpoort",
                    EigenaarId = context.Users.First().Id,
                    Straat = "Gentpoortstraat",
                    Nummer = "48",
                    Gemeente = "Brugge",
                    Afbeelding = "https://farm9.staticflickr.com/8840/18259403910_6138f0a862.jpg",
                    Email = "musea@brugge.be",
                    Telefoon = "+32 50 44 87 43",
                    Postcode = 8000,
                    MinLeeftijd = 9,
                    MaxLeeftijd = 12,
                    Tags = new List<Tag>()
                    {
                        tags[0],
                        tags[1],
                        tags[2],
                        tags[3]
                    }
                };
                Poi poi2 = new Poi()
                {
                    Naam = "Sint-Janshuismolen",
                    Straat = "Kruisvest",
                    Nummer = "",
                    Eigenaar = context.Users.First(),
                    Gemeente = "Brugge",
                    Postcode = 8000,
                    Email = "musea@brugge.be",
                    Afbeelding = "https://farm9.staticflickr.com/8888/18420708676_0fda078f15.jpg",
                    Telefoon = "+32 50 44 87 43",
                    MinLeeftijd = 11,
                    MaxLeeftijd = 11,
                    Tags = new List<Tag>(){
                        tags[4]

                    }
                    
                };
                context.Poi.Add(poi1);
                context.Poi.Add(poi2);
                context.SaveChanges();


                Activiteit activiteit2 = new Activiteit()
                {
                    PoiId = poi1.ID,
                    Naam = "Museum in een van de stadspoorten",
                    Boeken = new List<Boek>(){
                        boek1
                        
                    },
                    DeelLijst = boek1.DeelLijst,
                    DitactischeToelichting = "Project: Maak een miniatuurmolen.",
                    Uitleg = "Overbrengingen: tandwielen en riemoverbrenging Binnenin de molen zijn zowel tandwieloverbrengingen, als een riemoverbrenging te zien.U kunt de leerlingen laten experimenteren met verschillende overbrengingen. Hiervoor kunt u gebruik maken van verschillende LEGO® sets rond overbrengingen.",
                    Prijs = 5,
                    MinLeeftijd = 9,
                    MaxLeeftijd = 10,
                    MinDuur = 50,
                    MaxDuur = 50,
                    Eigenaar = context.Users.First(),
                    Tags = new List<Tag>(){
                        tagsActiviteit2[0],
                        tagsActiviteit2[1],
                        tagsActiviteit2[2]
                        
                    },
                    
                };
                context.Activiteiten.Add(activiteit2);
                context.SaveChanges();

                Activiteit activiteit = new Activiteit()
                {
                    PoiId = poi1.ID,
                    Naam = "Museum in een van de stadspoorten",
                    Boeken = new List<Boek>(){
                        boek2
                    },
                    DeelLijst = boek2.DeelLijst,
                    DitactischeToelichting = "Geschiedenis van Brugge (eigen streek)",
                    Uitleg = "Voor een groep/klas doet het museum zijn deuren open. Wel eerst eens bellen.",
                    Prijs = 0,
                    MinLeeftijd = 11,
                    MaxLeeftijd = poi1.MaxLeeftijd,
                    MinDuur = 120,
                    MaxDuur = 180,
                    Eigenaar = context.Users.First(),
                    Tags = new List<Tag>(){
                        tagsActiviteit[0],
                        tagsActiviteit[1],
                        tagsActiviteit[2],
                        tagsActiviteit[3],
                        tagsActiviteit[4],
                        tagsActiviteit[5]
                        
                    },
                    Benodigdheden= new List<Benodigdheid>(){
                        benodigdheden[0],
                        benodigdheden[1]
                    }
                };
                context.Activiteiten.Add(activiteit);
                context.SaveChanges();

            }

            


            #endregion

        }
    }
}
