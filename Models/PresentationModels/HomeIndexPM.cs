using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PresentationModels
{
    public class HomeIndexPM
    {
        public List<Boek> BoekenEigenaar { get; set; }
        public List<Boek> BoekenGedeeld { get; set; }
    }
}
