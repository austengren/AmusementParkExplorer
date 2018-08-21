using AmusementParkExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Contracts
{
    public interface IParkService
    {
        bool CreatePark(ParkCreate model);
        bool UpdatePark(ParkEdit model);
        bool DeletePark(int ParkID);
        IEnumerable<ParkListItem> GetParks();
        ParkDetail GetParkById(int parkId);
    }
}
