﻿using System.Data.Entity;
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
        public DbSet<Poi> Poi { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Uitnodiging> Uitnodigingen { get; set; }
        public DbSet<PoiTags> PoiTags { get; set; }
        public DbSet<BoekOrder> BoekOrder { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<RouteListItem> RouteListItem { get; set; }
        public DbSet<Vraag> Vragen { get; set; }




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
                       cs.ToTable("AspNetUserActiviteiten");
                   });
            modelBuilder.Entity<Route>()
                   .HasMany<ApplicationUser>(s => s.DeelLijst)
                   .WithMany(c => c.Routes)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("Route_Id");
                       cs.MapRightKey("AspNetUser_Id");
                       cs.ToTable("AspNetUserRoutes");
                   });
            modelBuilder.Entity<Boek>()
                   .HasMany<ApplicationUser>(s => s.DeelLijst)
                   .WithMany(c => c.Boeken)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("Boek_Id");
                       cs.MapRightKey("AspNetUser_Id");
                       cs.ToTable("AspNetUserBoeken");
                   });


            modelBuilder.Entity<PoiTags>()
              .HasKey(cp => new { cp.PoiId, cp.TagId });

            modelBuilder.Entity<Poi>()
                        .HasMany(c => c.Tags)
                        .WithRequired()
                        .HasForeignKey(cp => cp.PoiId);

            modelBuilder.Entity<Tag>()
                        .HasMany(p => p.Pois)
                        .WithRequired()
                        .HasForeignKey(cp => cp.TagId);

            modelBuilder.Entity<Benodigdheid>()
                   .HasMany<Activiteit>(s => s.Activiteiten)
                   .WithMany(c => c.Benodigdheden)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("Benodigdheid_Id");
                       cs.MapRightKey("Activiteit_Id");
                       cs.ToTable("BenodigdheidActiviteiten");
                   });
            modelBuilder.Entity<Boek>()
                  .HasMany<Activiteit>(s => s.Activiteiten)
                  .WithMany(c => c.Boeken)
                  .Map(cs =>
                  {
                      cs.MapLeftKey("Boek_Id");
                      cs.MapRightKey("Activiteit_Id");
                      cs.ToTable("BoekActiviteiten");
                  });
            modelBuilder.Entity<Boek>()
                   .HasMany<Route>(s => s.Routes)
                   .WithMany(c => c.Boeken)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("Boek_Id");
                       cs.MapRightKey("Route_Id");
                       cs.ToTable("BoekRoutes");
                   });
           
            modelBuilder.Entity<Tag>()
                 .HasMany<Activiteit>(s => s.Activiteiten)
                 .WithMany(c => c.Tags)
                 .Map(cs =>
                 {
                     cs.MapLeftKey("Tag_ID");
                     cs.MapRightKey("Activiteit_Id");
                     cs.ToTable("TagActiviteiten");
                 });
            modelBuilder.Entity<Video>()
                 .HasMany<Activiteit>(s => s.Activiteiten)
                 .WithMany(c => c.Videos)
                 .Map(cs =>
                 {
                     cs.MapLeftKey("Video_ID");
                     cs.MapRightKey("Activiteit_Id");
                     cs.ToTable("VideoActiviteiten");
                 });

        }
    }
}