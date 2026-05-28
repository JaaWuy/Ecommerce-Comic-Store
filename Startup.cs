using System;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using DoAnWeb.Models;

[assembly: OwinStartup(typeof(DoAnWeb.Startup))]

namespace DoAnWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles()
        {
            try
            {
                using (var context = new DSTruyen())
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                    // Tạo role Admin nếu chưa có
                    if (!roleManager.RoleExists("Admin"))
                    {
                        var role = new IdentityRole();
                        role.Name = "Admin";
                        roleManager.Create(role);
                    }

                    // Tạo role User nếu chưa có
                    if (!roleManager.RoleExists("User"))
                    {
                        var role = new IdentityRole();
                        role.Name = "User";
                        roleManager.Create(role);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error or ignore to prevent app crash
                System.Diagnostics.Debug.WriteLine("Error creating roles: " + ex.Message);
            }
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            // Cấu hình Database Context và User Manager
            app.CreatePerOwinContext(DSTruyen.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Cấu hình Cookie Authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
        }
    }
}
