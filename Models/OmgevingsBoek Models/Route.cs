﻿using Models.MVC_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OmgevingsBoek_Models
{
    public class Route
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public virtual ApplicationUser Eigenaar { get; set; }
        public int EigenaarID { get; set; }
        public virtual List<ApplicationUser> DeelLijst { get; set; }
        public virtual List<RouteListItem> RouteLijst { get; set; }
    }
}