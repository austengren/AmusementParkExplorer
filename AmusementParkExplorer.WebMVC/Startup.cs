using AmusementParkExplorer.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmusementParkExplorer.WebMVC.Startup))]
namespace AmusementParkExplorer.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@admin.com";

                string password = "Test1!";

                var checkUser = userManager.Create(user, password);

                if (checkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Subscriber"))
            {
                var role = new IdentityRole();
                role.Name = "Subscriber";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("TrialUser"))
            {
                var role = new IdentityRole();
                role.Name = "TrialUser";
                roleManager.Create(role);
            }
        }
    }
}
