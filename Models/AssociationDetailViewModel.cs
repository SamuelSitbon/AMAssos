using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMAssos.Models
{
    public class AssociationDetailViewModel
    {
        public Association Association { get; set; }
        public Dictionary<string, List<Sortie>> Sorties { get; set; }
    }
}