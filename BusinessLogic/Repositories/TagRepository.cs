using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class TagRepository : GenericRepository<Tag>, BusinessLogic.Repositories.ITagRepository
    {
        public TagRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public TagRepository()
            : base(new ApplicationDbContext())
        {

        }

        public Tag Insert(string entity)
        {
            Tag tag = context.Tags.Select(t => t).Where(t => t.Naam.Equals(entity)).SingleOrDefault();
            if (tag != null) return tag;
            Tag newtag = context.Tags.Add(new Tag() { Naam = entity });
            context.SaveChanges();
            return newtag;
        }
        
        public override IEnumerable<Tag> All()
        {
            return base.All();
        }


    }
}
