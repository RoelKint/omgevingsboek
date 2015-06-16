using Models.MVC_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    [Table("BoekOrder")]
    public class BoekOrder
    {
        public int Id { get; set; }
        public virtual ApplicationUser Eigenaar { get; set; }
        public string EigenaarId { get; set; }
        public int Index { get; set; }
        public virtual Boek Boek { get; set; }
        public int BoekId { get; set; }
        public bool IsSharedLijst { get; set; }

    }
}
