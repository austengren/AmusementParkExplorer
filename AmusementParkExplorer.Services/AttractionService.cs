using AmusementParkExplorer.Contracts;
using AmusementParkExplorer.Data;
using AmusementParkExplorer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Services
{
    public class AttractionService : IAttractionService
    {
        private readonly Guid _userID;
        private ParkService _parkService;
        private AttractionTypeService _attractionTypeService;

        public AttractionService(Guid userID)
        {
            _userID = userID;
            _parkService = new ParkService(_userID);
            _attractionTypeService = new AttractionTypeService(_userID);
        }

        public bool CreateAttraction(AttractionCreate model)
        {
            var entity =
                new Attraction()
                {
                    OwnerID = _userID,
                    AttractionName = model.AttractionName,
                    ParkID = model.ParkID,
                    AttractionTypeID = model.AttractionTypeID,
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

        public IEnumerable<AttractionListItem> GetAttractions()
       {
           using (var ctx = new ApplicationDbContext())
           {
               var query =
                    ctx
                        .Attractions
                        .Where(e => e.OwnerID == _userID)
                        .Include(e => e.ParkID)
                        .Include(e => e.AttractionTypeID)
                        .Select(
                            e =>
                               new AttractionListItem
                                {
                                    AttractionID = e.AttractionID,
                                    ParkID = e.ParkID,
                                    AttractionTypeID = e.AttractionTypeID,
                                    ParkName = e.Park.ParkName,
                                    City = e.Park.City,
                                    State = e.Park.State,
                                    AttractionTypeName = e.AttractionType.AttractionTypeName,
                                    AttractionName = e.AttractionName,
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
                        ParkName = entity.Park.ParkName,
                        AttractionName = entity.AttractionName,
                        AttractionTypeName = entity.AttractionType.AttractionTypeName,
                        AttractionRating = entity.AttractionRating,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
    }
}
