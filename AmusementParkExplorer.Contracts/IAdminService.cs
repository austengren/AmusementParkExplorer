using AmusementParkExplorer.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementParkExplorer.Contracts
{
    public interface IAdminService
    {
        IEnumerable<ApplicationUser> GetUserList();
        IEnumerable<IdentityRole> GetRolesList();
        bool IsAdminUser();
    }
}
