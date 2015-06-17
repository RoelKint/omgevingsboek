using Models.MVC_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    [Table("Routes")]
    public class Route
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public virtual ApplicationUser Eigenaar { get; set; }
        public string EigenaarID { get; set; }
        public virtual List<Boek> Boeken{ get; set; }
        public virtual List<ApplicationUser> DeelLijst { get; set; }
        public virtual List<RouteListItem> RouteLijst { get; set; }
        public bool IsDeleted { get; set; }

    }
}
