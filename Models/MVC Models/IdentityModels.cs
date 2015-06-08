using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.Collections.Generic;
using Models.OmgevingsBoek_Models;

namespace Models.MVC_Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public bool Deleted { get; set; }
        public string Afbeelding { get; set; }
        // Voor de aanmaak van de tussentabellen voor te delen.

        public virtual List<Activiteit> Activiteiten { get; set; }
        public List<int> ActiviteitenIds { get; set; }

        public virtual List<Route> Routes { get; set; }
        public List<int> RoutesIds { get; set; }

        public virtual List<Boek> Boeken { get; set; }
        public List<int> BoekenIds { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Activiteit> Activiteiten { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Benodigdheid> Benodigdheden { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Boek> Boeken { get; set; }
        public DbSet<Fotoboek> Fotoboeken { get; set; }
        public DbSet<Poi> Poi { get; set; }
        public DbSet<Video> Videos { get; set; }


        public ApplicationDbContext()
            : base("ApplicationDbContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Activiteit>()
                   .HasMany<ApplicationUser>(s => s.DeelLijst)
                   .WithMany(c => c.Activiteiten)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("Activiteit_Id");
                       cs.MapRightKey("AspNetUser_Id");
                       cs.ToTable("AspNetUserActiviteit");
                   });
            modelBuilder.Entity<Route>()
                   .HasMany<ApplicationUser>(s => s.DeelLijst)
                   .WithMany(c => c.Routes)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("Route_Id");
                       cs.MapRightKey("AspNetUser_Id");
                       cs.ToTable("AspNetUserRoute");
                   });
            modelBuilder.Entity<Boek>()
                   .HasMany<ApplicationUser>(s => s.DeelLijst)
                   .WithMany(c => c.Boeken)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("Boek_Id");
                       cs.MapRightKey("AspNetUser_Id");
                       cs.ToTable("AspNetUserBoek");
                   });
             
        }
    }
}