using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    public class RouteListItem
    {
        //ofwel adres ofwel activiteitId. adres is voor tussenstops bij activiteiten
        public int Id { get; set; }
        public int RouteId { get; set; }
        public virtual Activiteit Activiteit { get; set; }
        public string Adres { get; set; }
        public int OrderIndex { get; set; }
    }
}
