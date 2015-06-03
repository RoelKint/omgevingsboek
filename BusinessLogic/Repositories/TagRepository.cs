using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
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

        public override Tag Insert(Tag entity)
        {
            Tag tag = context.Tags.Select(t => t).Where(t => t.Naam.Equals(entity.Naam)).Single();
            if (tag != null) return tag;
            return context.Tags.Add(entity);
        }
        public override IEnumerable<Tag> All()
        {
            return base.All();
        }


    }
}
