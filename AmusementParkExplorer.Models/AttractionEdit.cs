using AmusementParkExplorer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Models
{
    public class AttractionEdit
    {
        public int AttractionID { get; set; }

        [Required]
        [Display(Name = "Park Name")]
        public int ParkID { get; set; }
        public string ParkName { get; set; }
        public virtual Park Park { get; set; }

        [Display(Name = "Attraction Name")]
        public string AttractionName { get; set; }

        [Required]
        [Display(Name = "Attraction Type")]
        public int AttractionTypeID { get; set; }
        public string AttractionTypeName { get; set; }
        public virtual AttractionType AttractionType { get; set; }

        [Display(Name = "Attraction Rating")]
        [Range(1, 5, ErrorMessage = "Please choose a number between 1 and 5")]
        public decimal AttractionRating { get; set; }
    }
}
