using AmusementParkExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Contracts
{
    public interface IAttractionService
    {
        bool CreateAttraction(AttractionCreate model);
        bool UpdateAttraction(AttractionEdit model);
        bool DeleteAttraction(int AttractionID);
        IEnumerable<AttractionListItem> GetAttractions();
        AttractionDetail GetAttractionById(int attractionID);
    }
}
