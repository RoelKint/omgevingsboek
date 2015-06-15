using Models;
using Models.MVC_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    [Table("Boeken")]
    public class Boek
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Afbeelding { get; set; }
        public virtual List<Activiteit> Activiteiten { get; set; }
        public virtual ApplicationUser Eigenaar { get; set; }
        public string EigenaarId { get; set; }
        public virtual List<ApplicationUser> DeelLijst { get; set; }
        public bool IsDeleted { get; set; }

    }
}
