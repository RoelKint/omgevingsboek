using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class BoekOrderRepository : GenericRepository<BoekOrder>, BusinessLogic.Repositories.IBoekOrderRepository
    {
        public BoekOrderRepository(ApplicationDbContext context)
            : base(context)
        {
            context.Configuration.LazyLoadingEnabled = false;
        }
        public BoekOrderRepository()
            : base(new ApplicationDbContext())
        {
            context.Configuration.LazyLoadingEnabled = false;
        }

        public List<BoekOrder> GetBoekOrderLijst(string Username, bool GedeeldeBoeken)
        {
            return (from b in context.BoekOrder where b.Eigenaar.UserName == Username where b.IsSharedLijst == GedeeldeBoeken orderby b.Index select b).ToList();

        }
        public BoekOrder GetBoekOrder(string id, int BoekId)
        {
            return (from b in context.BoekOrder where b.Eigenaar.Id == id where b.BoekId == BoekId select b).FirstOrDefault();
        }
        public List<BoekOrder> UpdateLijst(List<BoekOrder> lijst)
        {
            List<BoekOrder> res = new List<BoekOrder>();
            foreach (BoekOrder order in lijst)
            {
                BoekOrder bo = GetBoekOrder(order.EigenaarId, order.BoekId);
                if (bo != null)
                {
                    Delete(bo);
                    bo = new BoekOrder()
                    {
                        BoekId = order.BoekId,
                        Index = order.Index,
                        EigenaarId = order.EigenaarId,
                        IsSharedLijst = bo.IsSharedLijst
                    };
                    Insert(bo);
                }
                else
                {
                    bo = new BoekOrder()
                    {
                        BoekId = order.BoekId,
                        Index = order.Index,
                        EigenaarId = order.EigenaarId,
                        IsSharedLijst = order.IsSharedLijst
                    };
                    Insert(bo);
                }
                res.Add(bo);
            }
            return res;
        }
        public override void Update(BoekOrder entityToUpdate)
        {
            base.Update(entityToUpdate);
            context.SaveChanges();
        }

        public override BoekOrder Insert(BoekOrder entity)
        {
            BoekOrder bo = base.Insert(entity);
            context.SaveChanges();
            return bo;
        }
        public override void Delete(BoekOrder entityToDelete)
        {
            base.Delete(entityToDelete);
            context.SaveChanges();
        }
    }
}
