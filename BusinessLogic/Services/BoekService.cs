using BusinessLogic.Repositories;
using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class BoekService : BusinessLogic.Services.IBoekService 
    {
        private ITagRepository repoTag = null;
        private IActiviteitRepository repoActiviteit = null;
        private IBoekRepository repoBoek = null;
        private IPoiRepository repoPoi = null;

        public BoekService(
            ITagRepository repoTag,
            IActiviteitRepository repoActiviteit,
            IBoekRepository repoBoek,
            IPoiRepository repoPoi
            )
        {
            this.repoActiviteit = repoActiviteit;
            this.repoBoek = repoBoek;
            this.repoTag = repoTag;
            this.repoPoi = repoPoi;
        }
        public List<Activiteit> GetActiviteitenList()
        {
            return repoActiviteit.All().ToList();
        }
        public Boek InsertBoek(Boek boek)
        {
            return repoBoek.Insert(boek);
        }
        public void EditBoek(Boek boek)
        {
            repoBoek.Update(boek);
        }
        public void DeleteBoek(Boek boek)
        {
            repoBoek.Delete(boek);
        }
        public Activiteit GetActiviteitById(int id)
        {
            return repoActiviteit.GetByID(id);
        }
        public List<Activiteit> GetActivitiesByUsername(string Username)
        {
            return repoActiviteit.getActivitiesByUsername(Username);
        }

        public List<Activiteit> GetSharedActivitiesByUsername(string Username)
        {
            return repoActiviteit.getSharedActivitiesByUsername(Username);
        }
        public List<Activiteit> GetSharedActivitiesByBookId(int BoekId, string Username)
        {
            return repoActiviteit.getSharedActivitiesByBookId(BoekId,Username);
        }
        public Boek GetBoekByID(object id)
        {
            return repoBoek.GetByID(id);
        }
        public List<Boek> GetBoekenByUser(string username)
        {
            return repoBoek.getBoekenByUser(username);
        }
        //sharedboeken omvatten niet de eigen boeken.
        public List<Boek> GetSharedBoeken(string username)
        {
            return repoBoek.getSharedBoeken(username);
        }
        public ApplicationUser GetUser(String Username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Users.Select(i => i).Where(i => i.UserName == Username).Single();
            }
        }
        public List<Tag> GetTagList()
        {
            return repoTag.All().ToList();
        }
        public Tag InsertTag(Tag tag)
        {
            return repoTag.Insert(tag);
        }
        public List<Poi> GetPoiList()
        {
            return repoPoi.All().ToList();
        }
        public Poi InsertPoi(Poi poi)
        {
            return repoPoi.Insert(poi);
        }

    }
}
