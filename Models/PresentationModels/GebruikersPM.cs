using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PresentationModels
{
    public class GebruikersPM
    {
        public List<UserActivities> UserActivities { get; set; }
        public List<Uitnodiging> Uitnodigingen { get; set; }
    }
}
