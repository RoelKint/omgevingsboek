using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PresentationModels
{
    public class UitnodigingFeedbackPM
    {
        public List<String> Correct { get; set; }
        public List<String> Foutief { get; set; }

        public List<String> Gebruikt { get; set; }
    }
}
