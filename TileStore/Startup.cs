using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TileStore.Models;

[assembly: OwinStartupAttribute(typeof(TileStore.Startup))]
namespace TileStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //AddUsersAndRoles();
        }

        private void AddUsersAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if(!roleManager.RoleExists("salesperson"))
            {
                var role = new IdentityRole()
                {
                    Name = "salesperson"
                };
                roleManager.Create(role);

                var user = new ApplicationUser()
                {
                    UserName = "salesperson",
                    Email = "salesperson@leemutech.com"
                };
                string userPWD = "Updating@1234";

                var chkUser = UserManager.Create(user, userPWD);
                
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "salesperson");

                }
            }

            if (!roleManager.RoleExists("cashier"))
            {
                var role = new IdentityRole()
                {
                    Name = "cashier"
                };
                roleManager.Create(role);

                var user = new ApplicationUser()
                {
                    UserName = "cashier",
                    Email = "cashier@leemutech.com"
                };
                string userPWD = "Updating@1234";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "cashier");

                }
            }
        }
    }
}
