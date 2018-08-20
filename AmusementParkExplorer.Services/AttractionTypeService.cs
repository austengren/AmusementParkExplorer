using AmusementParkExplorer.Data;
using AmusementParkExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Services
{
    public class AttractionTypeService
    {
        private readonly Guid _userID;
        
         public AttractionTypeService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateAttractionType(AttractionTypeCreate model)
        {
            var entity =
                new AttractionType()
                {
                    OwnerID = _userID,
                    AttractionTypeName = model.AttractionTypeName,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.AttractionTypes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateAttractionType(AttractionTypeEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .AttractionTypes
                        .Single(e => e.AttractionTypeID == model.AttractionTypeID && e.OwnerID == _userID);

                entity.AttractionTypeName = model.AttractionTypeName;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAttractionType(int AttractionTypeID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .AttractionTypes
                        .Single(e => e.AttractionTypeID == AttractionTypeID && e.OwnerID == _userID);

                ctx.AttractionTypes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AttractionTypeListItem> GetAttractionTypes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .AttractionTypes
                        .Select(
                            e =>
                                new AttractionTypeListItem
                                {
                                    AttractionTypeID = e.AttractionTypeID,
                                    AttractionTypeName = e.AttractionTypeName,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public AttractionTypeDetail GetAttractionTypeById(int attractionTypeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .AttractionTypes
                        .Single(e => e.AttractionTypeID == attractionTypeId);
                return
                    new AttractionTypeDetail
                    {
                        AttractionTypeID = entity.AttractionTypeID,
                        AttractionTypeName = entity.AttractionTypeName,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
    }
}
