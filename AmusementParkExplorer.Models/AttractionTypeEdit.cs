using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Models
{
    public class AttractionTypeEdit
    {
        public int AttractionTypeID { get; set; }

        [Display(Name = "Attraction Type")]
        public string AttractionTypeName { get; set; }
    }
}
