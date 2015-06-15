using Models.MVC_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    [Table("PoiTags")]
    public class PoiTags
    {
        public virtual Tag Tag { get; set; }
        public int TagId { get; set; }
        public virtual Poi Poi { get; set; }
        public int PoiId { get; set; }
        public virtual ApplicationUser Eigenaar { get; set; }
        public string EigenaarId { get; set; }

    }
}
