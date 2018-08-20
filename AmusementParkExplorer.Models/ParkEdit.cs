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
    }
}
