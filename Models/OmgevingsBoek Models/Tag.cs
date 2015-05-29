using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    public class Tag
    {
        public int ID { get; set; }
        public int Naam { get; set; }
        public virtual List<Activiteit> Activiteiten { get; set; }
        public List<int> ActiviteitenIds { get; set; }
    }
}
