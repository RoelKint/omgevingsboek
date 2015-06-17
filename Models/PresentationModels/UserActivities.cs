using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PresentationModels
{
    public class UserActivities
    {
        public ApplicationUser User { get; set; }
        public List<Activiteit> Activiteiten { get; set; }
        public List<Boek> Boeken { get; set; }
        public string Role { get; set; }
    }
}
