using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Models
{
    public class AttractionTypeCreate
    {
        [Required]
        [Display(Name = "Attraction Type")]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string AttractionTypeName { get; set; }

        public override string ToString() => AttractionTypeName;
    }
}
