﻿using AmusementParkExplorer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Models
{
    public class AttractionDetail
    {
        public int ParkID { get; set; }

        [Display(Name = "Park Name")]
        public string ParkName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public virtual Park Park { get; set; }

        [Display(Name = "Attraction ID")]
        public int AttractionID { get; set; }

        [Display(Name = "Attraction Name")]
        public string AttractionName { get; set; }

        public int AttractionTypeID { get; set; }

        [Display(Name = "Attraction Type")]
        public string AttractionTypeName { get; set; }
        public virtual AttractionType AttractionType { get; set; }

        [Display(Name = "Rating")]
        public decimal AttractionRating { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }

        public override string ToString() => $"[{AttractionID}] {AttractionName}";
    }
}
