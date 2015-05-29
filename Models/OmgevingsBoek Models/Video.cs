using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    public class Video
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public virtual List<Activiteit> Activiteiten { get; set; }
    }
}
