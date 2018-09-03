using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Data
{
    public class Attraction
    {
        [Key]
        public int AttractionID { get; set; }

        [Required]
        public int ParkID { get; set; }

        public virtual Park Park { get; set; }

        [Required]
        public Guid OwnerID { get; set; }

        [Required]
        public string AttractionName { get; set; }

        [Required]
        public int AttractionTypeID { get; set; }

        public virtual AttractionType AttractionType { get; set; }

        [Required]
        public decimal AttractionRating { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
