using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace BusinessLogic.Repositories
{
    public class PoiRepository : GenericRepository<Poi>, BusinessLogic.Repositories.IPoiRepository
    {
        public PoiRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public PoiRepository()
            : base(new ApplicationDbContext())
        {
            context.Configuration.LazyLoadingEnabled = false;

        }
        

        public override IEnumerable<Poi> All()
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Poi.Include(p => p.Tags.Select(t => t.Tag)).Where(p => p.IsDeleted == false);
        }
        public override Poi GetByID(object id)
        {
            return this.context.Poi.Select(p => p).Where(p => !p.IsDeleted).Include(p => p.Tags.Select(t => t.Tag)).Where(p => p.ID == (int)id).SingleOrDefault();
        }
        public Poi Insert(Poi entity)
        {
            List<PoiTags> tagsPoi = entity.Tags;
            entity.Tags = new List<PoiTags>();

            Poi poi = context.Poi.Add(entity);
            try
            {
                context.SaveChanges();
                poi.Tags = new List<PoiTags>();
                foreach (PoiTags tag in tagsPoi)
                {
                    poi.Tags.Add(
                    new PoiTags()
                    {
                        TagId = tag.TagId,
                        PoiId = poi.ID,
                        EigenaarId = poi.EigenaarId
                    });
                }
                Update(poi);

            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            return poi;
        }
        public List<Poi> get50FromSortNameAZ(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags.Select(t => t.Tag)).Include(p => p.Eigenaar).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderBy(i => i.Naam).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortNameZA(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags.Select(t => t.Tag)).Include(p => p.Eigenaar).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderByDescending(i => i.Naam).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortEmailAZ(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags.Select(t => t.Tag)).Include(p => p.Eigenaar).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderBy(i => i.Email).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortEmailZA(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags.Select(t => t.Tag)).Include(p => p.Eigenaar).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderByDescending(i => i.Email).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortAddressAZ(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags.Select(t => t.Tag)).Include(p => p.Eigenaar).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderBy(i => i.Straat).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortAddressZA(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags.Select(t => t.Tag)).Include(p => p.Eigenaar).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderByDescending(i => i.Straat).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortDeletedAZ(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags.Select(t => t.Tag)).Include(p => p.Eigenaar).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderBy(i => i.IsDeleted).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortDeletedZA(int from, string search, bool DisplayDeleted)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags.Select(t => t.Tag)).Include(p => p.Eigenaar).Where(i => !i.IsDeleted || i.IsDeleted == DisplayDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderByDescending(i => i.IsDeleted).Skip(from).Take(30).ToList();
        }
        public void Delete(Poi EntityToDelete)
        {
            List<Activiteit> acs = context.Activiteiten.Where(w => w.PoiId == EntityToDelete.ID).ToList();
            foreach (Activiteit a in acs)
            {
                a.IsDeleted = true;
            }
            base.Delete(EntityToDelete);
            context.SaveChanges();
        }
        public override void Update(Poi entityToUpdate)
        {
            base.Update(entityToUpdate);
            context.SaveChanges();
        }

        public void AddTag(int PoiId, int TagId,string UserName)
        {
            Poi poi = GetByID(PoiId);
            if (poi.Tags == null) poi.Tags = new List<PoiTags>();
            PoiTags pt = context.PoiTags.Add(new PoiTags()
            {
                TagId = TagId,
                PoiId = PoiId,
                EigenaarId = context.Users.Where(u => u.UserName == UserName).FirstOrDefault().Id
            });
            poi.Tags.Add(pt);
            Update(poi);
            
            
        }

        public void DeleteSoft(Poi poi)
        {

            poi.IsDeleted = true;
            Update(poi);
            
        }
        public List<PoiTags> GetTags(int PoiId)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Poi.Include(p => p.Tags.Select(t => t.Eigenaar)).Include(p => p.Tags.Select(t => t.Tag)).Where(p => p.ID == PoiId).SingleOrDefault().Tags;
        }
        public void DeleteTag(int PoiTagId)
        {
            context.SaveChanges();
        }
        public void UpdateFoto(int PoiId, string foto)
        {
            Poi p = GetByID(PoiId);
            p.Afbeelding = foto;
            Update(p);
        }
        
    }
}
