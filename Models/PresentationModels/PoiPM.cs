﻿using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PresentationModels
{
    public class PoiPM
    {
        public Poi poi { get; set; }
        public string Afbeelding { get; set; }
        List<Activiteit> Activiteiten { get; set; }

    }
}
