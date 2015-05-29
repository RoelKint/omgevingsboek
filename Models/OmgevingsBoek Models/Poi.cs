using Models.MVC_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    public class Poi
    {
        public int ID { get; set; }
        [Required]
        public string Naam { get; set; }
        public virtual ApplicationUser Eigenaar { get; set; }
        public string EigenaarId { get; set; }
        public string GeoLocatie { get; set; }
        public string Email { get; set; }
        public string Telefoon { get; set; }
        public string Straat { get; set; }
        public string Nummer { get; set; }
        public string Gemeente { get; set; }
        public int Postcode { get; set; }
        public int MinLeeftijd { get; set; }
        public int MaxLeeftijd { get; set; }
        public double Prijs { get; set; }
        public virtual List<Tag> Tags { get; set; }

    }
}
