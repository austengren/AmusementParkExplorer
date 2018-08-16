using AmusementParkExplorer.Data;
using AmusementParkExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Services
{
    public class ParkService
    {
        private readonly Guid _userID;

        public ParkService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreatePark(ParkCreate model)
        {
            var entity =
                new Park()
                {
                    OwnerID = _userID,
                    ParkName = model.ParkName,
                    City = model.City,
                    State = model.State,
                    ParkRating = model.ParkRating,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Parks.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdatePark(ParkEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Parks
                        .Single(e => e.ParkID == model.ParkID && e.OwnerID == _userID);

                entity.ParkName = model.ParkName;
                entity.City = model.City;
                entity.State = model.State;
                entity.ParkRating = model.ParkRating;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePark(int ParkID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Parks
                        .Single(e => e.ParkID == ParkID && e.OwnerID == _userID);

                ctx.Parks.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ParkListItem> GetParks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Parks
                        .Where(e => e.OwnerID == _userID)
                        .Select(
                            e =>
                                new ParkListItem
                                {
                                    ParkID = e.ParkID,
                                    ParkName = e.ParkName,
                                    City = e.City,
                                    State = e.State,
                                    ParkRating = e.ParkRating,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public ParkDetail GetParkById(int parkId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Parks
                        .Single(e => e.ParkID == parkId && e.OwnerID == _userID);
                return
                    new ParkDetail
                    {
                        ParkID = entity.ParkID,
                        ParkName = entity.ParkName,
                        City = entity.City,
                        State = entity.State,
                        ParkRating = entity.ParkRating,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
    }
}
