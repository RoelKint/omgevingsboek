using Models.MVC_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    [Table("Activiteiten")]
    public class Activiteit
    {
        public int Id { get; set; }
        public virtual ApplicationUser Eigenaar { get; set; }
        public string EigenaarId { get; set; }
        [Required]
        public String Naam { get; set; }
        public virtual Poi Poi { get; set; }
        [Required]
        public int PoiId { get; set; }
        [Required]
        public int MinLeeftijd { get; set; }
        [Required]
        public int MaxLeeftijd { get; set; }
        [Required]
        public int MinDuur { get; set; }
        [Required]
        public int MaxDuur { get; set; }
        public bool IsDeleted { get; set; }
        public double Prijs { get; set; }
        public string AfbeeldingNaam { get; set; }
        public string DitactischeToelichting  { get; set; }
        public string Uitleg { get; set; }
        public virtual List<ApplicationUser> DeelLijst { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public virtual List<Benodigdheid> Benodigdheden { get; set; }
        public virtual List<Route> Routes { get; set; }
        public virtual List<Video> Videos { get; set; }
        public virtual List<Fotoboek> Fotoboeken { get; set; }
        public virtual List<Boek> Boeken { get; set; }
        
    }
}
