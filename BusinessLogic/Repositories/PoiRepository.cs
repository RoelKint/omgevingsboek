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
            return context.Poi.Include(p => p.Tags);
        }
        public override Poi GetByID(object id)
        {
            return this.context.Poi.Select(p => p).Where(p => !p.IsDeleted).Include(p => p.Tags).Where(p => p.ID == (int)id).SingleOrDefault();
        }
        public Poi Insert(Poi entity)
        {
            List<Tag> tags = new List<Tag>();

            foreach (Tag tag in entity.Tags)
            {
                tags.Add(context.Tags.Find(tag.ID));
            }
            entity.Tags = tags;

            Poi poi = context.Poi.Add(entity);
            try
            {
                context.SaveChanges();
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
        public List<Poi> get50FromSortNameAZ(int from, string search)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags).Include(p => p.Eigenaar).Where(i => !i.IsDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderBy(i => i.Naam).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortNameZA(int from, string search)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags).Include(p => p.Eigenaar).Where(i => !i.IsDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderByDescending(i => i.Naam).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortEmailAZ(int from, string search)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags).Include(p => p.Eigenaar).Where(i => !i.IsDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderBy(i => i.Email).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortEmailZA(int from, string search)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags).Include(p => p.Eigenaar).Where(i => !i.IsDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderByDescending(i => i.Email).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortAddressAZ(int from, string search)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags).Include(p => p.Eigenaar).Where(i => !i.IsDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderBy(i => i.Straat).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortAddressZA(int from, string search)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Poi.Include(p => p.Tags).Include(p => p.Eigenaar).Where(i => !i.IsDeleted).Where(p => p.Naam.Contains(search) || p.Eigenaar.UserName.Contains(search) || p.Straat.Contains(search) || p.Gemeente.Contains(search) || p.Telefoon.Contains(search)).OrderByDescending(i => i.Straat).Skip(from).Take(30).ToList();
        }
        public void Delete(Poi EntityToDelete)
        {
            base.Delete(EntityToDelete);
            context.SaveChanges();
        }
        public override void Update(Poi entityToUpdate)
        {
            base.Update(entityToUpdate);
            context.SaveChanges();
        }

        public void AddTag(int PoiId, int TagId)
        {
            Poi poi = GetByID(PoiId);
            if (poi.Tags == null) poi.Tags = new List<Tag>();
            poi.Tags.Add(context.Tags.Where(t => t.ID == TagId).FirstOrDefault());
            Update(poi);
            
            
        }

        public void DeleteSoft(Poi poi)
        {
            poi.IsDeleted = true;
            Update(poi);
            
        }
        public List<Tag> GetTags(int PoiId)
        {
            return GetByID(PoiId).Tags;
        }

    }
}
