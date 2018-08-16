using AmusementParkExplorer.Data;
using AmusementParkExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Services
{
    public class AttractionService
    {
        private readonly Guid _userID;
        
         public AttractionService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateAttraction(AttractionCreate model)
        {
            var entity =
                new Attraction()
                {
                    OwnerID = _userID,
                    AttractionName = model.AttractionName,
                    ParkID = model.ParkID,
                    AttractionType = model.AttractionType,
                    AttractionRating = model.AttractionRating,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Attractions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateAttraction(AttractionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Attractions
                        .Single(e => e.AttractionID == model.AttractionID && e.OwnerID == _userID);

                entity.AttractionName = model.AttractionName;
                entity.AttractionType = model.AttractionType;
                entity.AttractionRating = model.AttractionRating;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAttraction(int AttractionID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Attractions
                        .Single(e => e.AttractionID == AttractionID && e.OwnerID == _userID);

                ctx.Attractions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AttractionListItem> GetAttraction()
       {
           using (var ctx = new ApplicationDbContext())
           {
               var query =
                    ctx
                        .Attractions
                        .Where(e => e.OwnerID == _userID)
                        .Select(
                            e =>
                               new AttractionListItem
                                {
                                    AttractionID = e.AttractionID,
                                    AttractionName = e.AttractionName,
                                    AttractionType = e.AttractionType,
                                    AttractionRating = e.AttractionRating,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public AttractionDetail GetAttractionById(int attractionID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Attractions
                        .Single(e => e.AttractionID == attractionID && e.OwnerID == _userID);
                return
                    new AttractionDetail
                    {
                        AttractionID = entity.AttractionID,
                        AttractionName = entity.AttractionName,
                        AttractionType = entity.AttractionType,
                        AttractionRating = entity.AttractionRating,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
    }
}
