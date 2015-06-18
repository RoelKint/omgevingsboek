﻿
using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class VraagRepository : GenericRepository<Vraag>, BusinessLogic.Repositories.IVraagRepository
    {
        public VraagRepository(ApplicationDbContext context)
            : base(context)
        {
            context.Configuration.LazyLoadingEnabled = false;
        }
        public VraagRepository()
            : base(new ApplicationDbContext())
        {
            context.Configuration.LazyLoadingEnabled = false;
        }

        public List<Vraag> GetVragen()
        {
            return context.Vragen.Where(v => !v.IsDeleted).ToList();
        }
        public override Vraag Insert(Vraag entity)
        {
            return base.Insert(entity);
        }
        public void Opgelost(int VraagId)
        {
            Vraag vraag = GetByID(VraagId);
            vraag.IsDeleted = true;
            Update(vraag);
            context.SaveChanges();
        }
        public override Vraag GetByID(object id)
        {
            return base.GetByID(id);
        }
        public void Gelezen(int VraagId)
        {
            Vraag vraag = GetByID(VraagId);
            vraag.IsGelezen = true;
            Update(vraag);
            context.SaveChanges();
        }

    }
}
