using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Data
{
    public class FoodDrink
    {
        [Key]
        public int FoodDrinkID { get; set; }

        [Required]
        public int ParkID { get; set; }

        public virtual Park Park { get; set; }

        [Required]
        public Guid OwnerID { get; set; }

        [Required]
        public string FoodDrinkName { get; set; }

        [Required]
        public string FoodDrinkType { get; set; }

        [Required]
        public decimal FoodDrinkRating { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
