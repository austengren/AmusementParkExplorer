﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Models
{
    public class AttractionDetail
    {
        [Display(Name = "Park Name")]
        public string ParkName { get; set; }

        [Display(Name = "Attraction ID")]
        public int AttractionID { get; set; }

        [Display(Name = "Attraction Name")]
        public string AttractionName { get; set; }

        [Display(Name = "Attraction Type")]
        public string AttractionType { get; set; }

        [Display(Name = "Attraction Rating")]
        public decimal AttractionRating { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }

        public override string ToString() => $"[{AttractionID}] {AttractionName}";
    }
}
