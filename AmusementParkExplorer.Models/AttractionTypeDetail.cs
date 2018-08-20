using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Models
{
    public class AttractionTypeDetail
    {
        [Display(Name = "Attraction Type ID")]
        public int AttractionTypeID { get; set; }

        [Display(Name = "Attraction Type Name")]
        public string AttractionTypeName { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }

        public override string ToString() => $"[{AttractionTypeID}] {AttractionTypeName}";
    }
}
