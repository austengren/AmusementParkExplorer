using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Data
{
    public class AttractionType
    {
        [Key]
        public int AttractionTypeID { get; set; }

        [Required]
        public Guid OwnerID { get; set; }

        [Required]
        public string AttractionTypeName { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
