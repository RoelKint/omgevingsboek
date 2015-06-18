using Models.MVC_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    public class Vraag
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Omschrhijving { get; set; }
        public virtual ApplicationUser Eigenaar { get; set; }
        public string EigenaarId { get; set; }
        public bool IsGelezen { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Datum { get; set; }
    }
}
