using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace BusinessLogic.Repositories
{
    public class ActiviteitRepository : GenericRepository<Activiteit>, BusinessLogic.Repositories.IActiviteitRepository
    {
        public ActiviteitRepository(ApplicationDbContext context)
            : base(context)
        {
            context.Configuration.LazyLoadingEnabled = false;
        }
        public ActiviteitRepository()
            : base(new ApplicationDbContext())
        {
            context.Configuration.LazyLoadingEnabled = false;
        }

        public override IEnumerable<Activiteit> All()
        {
            return this.context.Activiteiten.Include(a => a.Boeken).Include(a => a.Benodigdheden).Include(a => a.DeelLijst).Include(a => a.Eigenaar).Include(a => a.Poi).Include(a => a.Routes).Include(a => a.Tags).Include(a => a.Videos);
        }

        public List<Activiteit> getActiviteitenPerPoi(int id)

        {
            context.Configuration.LazyLoadingEnabled = false;
            return (from a in context.Activiteiten where !a.IsDeleted where a.PoiId == id select a).ToList();
        }

        public override Activiteit GetByID(object id)
        {
            return this.context.Activiteiten.Where(a => a.Id == (int)id).Where(i => !i.IsDeleted).Include(a => a.DeelLijst).Include(a => a.Eigenaar).Include(a => a.Benodigdheden).Include(a => a.Videos).Include(a => a.Fotos).Include(a => a.Tags).SingleOrDefault();
        }
        public Activiteit GetByIDAdmin(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Activiteiten.Where(a => a.Id == (int)id).FirstOrDefault();
        }
        public List<Activiteit> getActivitiesByUsername(string Username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from a in context.Activiteiten.Include(a => a.Poi) where !a.IsDeleted where a.Eigenaar.UserName == Username select a).ToList();
            }
        }
        public List<Activiteit> getSharedActivitiesByUsername(string Username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from a in context.Activiteiten
                            .Include(a => a.Eigenaar)
                            .Include(a => a.Poi)
                        where a.DeelLijst.Contains(context.Users.Select(i=>i).Where(i=>i.UserName == Username).FirstOrDefault())
                        where !a.IsDeleted
                        select a).ToList();
            }
        }
        public List<Activiteit> getSharedActivitiesByBookId(int BoekId,string Username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return (from a in context.Activiteiten.Include(a => a.Boeken)
                            .Include(a => a.Eigenaar)
                        where a.Boeken.Contains(context.Boeken.Select(b => b).Where(b => b.Id == BoekId).FirstOrDefault())
                        where !a.IsDeleted
                        select a).ToList();
            }
        }
        
        public List<Activiteit> get50FromSortNameAZ(int from,string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Activiteiten.Include(a => a.Eigenaar).Where(a => a.Poi.Naam.Contains(search) || a.Naam.Contains(search) || a.Eigenaar.UserName.Contains(search)).Include(a => a.Poi).Include(a => a.Eigenaar).Where(i => i.IsDeleted == false || i.IsDeleted == DisplayDeleted).OrderBy(a => a.Naam).Skip(from).Take(30).ToList();
        }
        public List<Activiteit> get50FromSortNameZA(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Activiteiten.Include(a => a.Eigenaar).Where(a => a.Poi.Naam.Contains(search) || a.Naam.Contains(search) || a.Eigenaar.UserName.Contains(search)).Include(a => a.Poi).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).OrderByDescending(a => a.Naam).Skip(from).Take(30).ToList();
        }
        public List<Activiteit> get50FromSortUserAZ(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Activiteiten.Include(a => a.Eigenaar).Include(a => a.Poi).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(a => a.Poi.Naam.Contains(search) || a.Naam.Contains(search) || a.Eigenaar.UserName.Contains(search)).OrderBy(a => a.Eigenaar.UserName).Skip(from).Take(30).ToList();
        }
        public List<Activiteit> get50FromSortUserZA(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Activiteiten.Include(a => a.Eigenaar).Include(a => a.Poi).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(a => a.Poi.Naam.Contains(search) || a.Naam.Contains(search) || a.Eigenaar.UserName.Contains(search)).OrderByDescending(a => a.Eigenaar.UserName).Skip(from).Take(30).ToList();
        }
        public List<Activiteit> get50FromSortPoiAZ(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Activiteiten.Include(a => a.Eigenaar).Include(a => a.Poi).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(a => a.Poi.Naam.Contains(search) || a.Naam.Contains(search) || a.Eigenaar.UserName.Contains(search)).OrderBy(a => a.Poi.Naam).Skip(from).Take(30).ToList();
        }
        public List<Activiteit> get50FromSortPoiZA(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Activiteiten.Include(a => a.Eigenaar).Include(a => a.Poi).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(a => a.Poi.Naam.Contains(search) || a.Naam.Contains(search) || a.Eigenaar.UserName.Contains(search)).OrderByDescending(a => a.Poi.Naam).Skip(from).Take(30).ToList();
        }
        public List<Activiteit> get50FromSortDeletedAZ(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Activiteiten.Include(a => a.Eigenaar).Include(a => a.Poi).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(a => a.Poi.Naam.Contains(search) || a.Naam.Contains(search) || a.Eigenaar.UserName.Contains(search)).OrderBy(a => a.IsDeleted).Skip(from).Take(30).ToList();
        }
        public List<Activiteit> get50FromSortDeletedZA(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Activiteiten.Include(a => a.Eigenaar).Include(a => a.Poi).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(a => a.Poi.Naam.Contains(search) || a.Naam.Contains(search) || a.Eigenaar.UserName.Contains(search)).OrderByDescending(a => a.IsDeleted).Skip(from).Take(30).ToList();
        }
        public List<Activiteit> getUserActiviteitenByUser50from(int from, String Owner, String Visitor)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Activiteiten.Include(a => a.Eigenaar).Include(a => a.Poi).Include(a => a.Eigenaar).Include(a => a.Poi).Where(i => !i.IsDeleted).Where(a => a.Eigenaar.UserName == Owner).Where(i => i.DeelLijst.Contains(context.Users.Select(u => u).Where(u => u.UserName == Visitor).FirstOrDefault())).OrderBy(a => a.Naam).Skip(from).Take(30).ToList();
        }


        public void DeleteSoft(Activiteit entityToDelete)
        {
            entityToDelete.IsDeleted = true;
            Update(entityToDelete);
            context.SaveChanges();

        }
        public bool IsActivityAccessibleByUser(int activiteitId, string Username) 
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ApplicationUser user = context.Users.Where(u => u.UserName == Username).FirstOrDefault();
                Activiteit d = context.Activiteiten.Where(a => a.Id == activiteitId).FirstOrDefault();
                return d.DeelLijst.Contains(user);

            }
        }

        public override void Delete(Activiteit id)
        {
            base.Delete(id);
            context.SaveChanges();

        }

        public List<Activiteit> getActiviteitenByPoiByUser50from(int from, String Owner, int PoiId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from a in context.Activiteiten where !a.IsDeleted where a.DeelLijst.Contains(context.Users.Where(u => u.UserName == Owner).FirstOrDefault()) where a.Poi.ID == PoiId select a).ToList();
            }
        }
        public override Activiteit Insert(Activiteit entity)
        {

            entity.DeelLijst = new List<ApplicationUser>();
            entity.DeelLijst.Add(context.Users.Where(u => u.Id == entity.EigenaarId).FirstOrDefault());

            List<Tag> tags = new List<Tag>();

            foreach (Tag tag in entity.Tags)
            {
                tags.Add(context.Tags.Find(tag.ID));
            }
            entity.Tags = tags;

            List<Benodigdheid> be = new List<Benodigdheid>();

            foreach (Benodigdheid b in entity.Benodigdheden)
            {
                be.Add(context.Benodigdheden.Find(b.Id));
            }
            entity.Benodigdheden = be;

            entity.Boeken = new List<Boek>(){
                context.Boeken.Find(entity.Boeken.FirstOrDefault().Id)
            };


            context.Activiteiten.Add(entity);
            context.SaveChanges();
            return entity;
        }
        public async void AddFotoToActiviteit(int ActiviteitId,string Foto)
        {
            Foto foto = context.Fotos.Add(new Foto() { FotoUrl = Foto });
            Activiteit a = context.Activiteiten.Where(i => i.Id == ActiviteitId).FirstOrDefault();
            if (a.Fotos == null)
                a.Fotos = new List<Models.OmgevingsBoek_Models.Foto>();
            a.Fotos.Add(foto);
            await context.SaveChangesAsync();
        }
        public void UpdateActiviteitFoto(int ActiviteitId, string foto)
        {
            Activiteit a = GetByID(ActiviteitId);
            a.AfbeeldingNaam = foto;
            Update(a);
            context.SaveChanges();
        }
        public void addUserToShareList(int Id, string Username, bool IsGedeeld)
        {
            Activiteit a = GetByID(Id);
            ApplicationUser user = context.Users.Where(u => u.UserName == Username).FirstOrDefault();
            
            if (IsGedeeld)
            {
                if (a.DeelLijst.Any(d => d.UserName == Username)) return;
                Boek deelboek;
                if (context.Boeken.Where(b => b.Naam == "Gedeelde Activiteiten").Where(b => b.DeelLijst.Any(x => x.UserName == user.UserName)).Where(b => b.EigenaarId == null).FirstOrDefault() == null)
                {
                    List<ApplicationUser> deellijst = new List<ApplicationUser>();
                    deellijst.Add(user);

                    deelboek = context.Boeken.Add(new Boek()
                    {
                        Naam = "Gedeelde Activiteiten",
                        DeelLijst = deellijst,
                        Afbeelding = ""
                    });

                    int previndex;
                    List<BoekOrder> l = context.BoekOrder.Where(i => i.EigenaarId == user.Id).Where(i => i.IsSharedLijst == true).ToList();
                    if (l.Count != 0)
                        previndex = context.BoekOrder.Where(i => i.EigenaarId == user.Id).Where(i => i.IsSharedLijst == true).Max(i => i.Index);
                    else
                        previndex = -1;
                    context.BoekOrder.Add(new BoekOrder()
                    {
                        BoekId = deelboek.Id,
                        EigenaarId = user.Id,
                        Index = -1,
                        IsSharedLijst = true
                    });

                }
                else
                {
                    deelboek = context.Boeken.Where(b => b.Naam == "Gedeelde Activiteiten").Include(x => x.Activiteiten).Where(b => b.DeelLijst.Any(x => x.UserName == user.UserName)).Where(b => b.EigenaarId == null).FirstOrDefault();
                }
                if (deelboek.Activiteiten == null) deelboek.Activiteiten = new List<Activiteit>();
                deelboek.Activiteiten.Add(a);

                a.DeelLijst.Add(user);
                Update(a);
                context.SaveChanges();
            }
            else
            {
                Boek deelboek = context.Boeken.Where(b => b.Naam == "Gedeelde Activiteiten").Include(x => x.Activiteiten).Where(b => b.DeelLijst.Any(x => x.UserName == user.UserName)).Where(b => b.EigenaarId == null).FirstOrDefault();
                deelboek.Activiteiten.Remove(a);
                a.DeelLijst.Remove(user);
                Update(a);
                context.SaveChanges();
            }
        }
        public void UpdateActiviteit(Activiteit activiteit)
        {
            Activiteit origineleActiviteit = GetByID(activiteit.Id);
            origineleActiviteit.AfbeeldingNaam = activiteit.AfbeeldingNaam;
            origineleActiviteit.DitactischeToelichting = activiteit.DitactischeToelichting;
            origineleActiviteit.MaxDuur = activiteit.MaxDuur;
            origineleActiviteit.MaxLeeftijd = activiteit.MaxLeeftijd;
            origineleActiviteit.MinDuur = activiteit.MinDuur;
            origineleActiviteit.MinLeeftijd = activiteit.MinLeeftijd;
            origineleActiviteit.Naam = activiteit.Naam;
            origineleActiviteit.PoiId = activiteit.PoiId;
            origineleActiviteit.Prijs = activiteit.Prijs;
            origineleActiviteit.Uitleg = activiteit.Uitleg;
            origineleActiviteit.Benodigdheden =new List<Benodigdheid>();
            foreach(Benodigdheid b in activiteit.Benodigdheden){
                origineleActiviteit.Benodigdheden.Add(context.Benodigdheden.Find(b));
            }
            origineleActiviteit.Fotos = new List<Foto>();
            foreach (Foto f in activiteit.Fotos)
            {
                origineleActiviteit.Fotos.Add(context.Fotos.Find(f));
            }
            origineleActiviteit.Tags = new List<Tag>();
            foreach (Tag t in activiteit.Tags)
            {
                origineleActiviteit.Tags.Add(context.Tags.Find(t));
            }
            Update(origineleActiviteit);
            context.SaveChanges();

        }
    }
}
