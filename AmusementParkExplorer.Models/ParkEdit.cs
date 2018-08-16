using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Models
{
    public class ParkEdit
    {
        public int ParkID { get; set; }

        [Display(Name = "Park Name")]
        public string ParkName { get; set; }

        public string City { get; set; }
        public string State { get; set; }

        [Display(Name = "Park Rating")]
        [Range(1, 5, ErrorMessage = "Please choose a number between 1 and 5")]
        public decimal ParkRating { get; set; }
    }
}
