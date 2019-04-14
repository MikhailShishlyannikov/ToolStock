using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.Initializers
{
    public static class AdminInitializer
    {
        public static void Initialize(ToolContext toolContext)
        {
            if (toolContext.Users.Any(u => u.UserName == "admin@gmail.com")) return;

            var admin = new UserModel {Email = "admin@gmail.com", UserName = "admin@gmail.com" };
            var password = "SuperAdmin";

            var adminUserInfo = new UserInfoModel
            {
                User = admin,
                Name = "Admin",
                Surname = "Admin",
                Email = admin.Email,
                Phone = "+375336340232"
            };

            var uManager = new UserManager<UserModel>(new UserStore<UserModel>(toolContext));
            var result = uManager.Create(admin, password);

            toolContext.UserInfos.Add(adminUserInfo);

            // if the user creation was successful
            if (result.Succeeded)
            {
                // create role for default admin
                uManager.AddToRole(admin.Id, toolContext.Roles.FirstOrDefault(r => r.Name == "Admin")?.Name);
            }
        }
    }
}
