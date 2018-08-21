using AmusementParkExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Contracts
{
    public interface IAttractionTypeService
    {
        bool CreateAttractionType(AttractionTypeCreate model);
        bool UpdateAttractionType(AttractionTypeEdit model);
        bool DeleteAttractionType(int AttractionTypeID);
        IEnumerable<AttractionTypeListItem> GetAttractionTypes();
        AttractionTypeDetail GetAttractionTypeById(int attractionTypeId);
    }
}
