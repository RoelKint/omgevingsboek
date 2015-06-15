using Models.MVC_Models;
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
        public string Naam { get; set; }
        public virtual List<Activiteit> Activiteiten { get; set; }
        public virtual List<PoiTags> Pois { get; set; }
        public bool IsDeleted { get; set; }

    }
}
