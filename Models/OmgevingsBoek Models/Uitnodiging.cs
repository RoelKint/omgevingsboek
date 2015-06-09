using Models.MVC_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    public class Uitnodiging
    {
        public int Id { get; set; }
        public virtual ApplicationUser Eigenaar { get; set; }
        public string EigenaarId { get; set; }
        public string Key { get; set; }
        public bool Gebruikt { get; set; }
        public virtual ApplicationUser GebruiktDoor { get; set; }
        public string GebruiktDoorId { get; set; }
        public string EmailUitgenodigde { get; set; }

    }
}
