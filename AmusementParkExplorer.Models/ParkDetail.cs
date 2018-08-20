using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Models
{
    public class ParkDetail
    {
        [Display(Name = "Park ID")]
        public int ParkID { get; set; }

        [Display(Name = "Park Name")]
        public string ParkName { get; set; }

        public string City { get; set; }
        public string State { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name ="Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }

        public override string ToString() => $"[{ParkID}] {ParkName}";
    }
}
