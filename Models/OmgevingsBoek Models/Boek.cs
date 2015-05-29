using Models;
using Models.MVC_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    public class Boek
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public virtual List<Activiteit> Activiteiten { get; set; }
        public virtual ApplicationUser Eigenaar { get; set; }
        public string EigenaarId { get; set; }
        public virtual List<ApplicationUser> DeelLijst { get; set; }
    }
}
