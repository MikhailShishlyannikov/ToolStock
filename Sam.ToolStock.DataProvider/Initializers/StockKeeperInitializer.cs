using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.Initializers
{
    public static class StockKeeperInitializer
    {
        public static void Initialize(ToolContext toolContext)
        {
            if(toolContext.Roles.First(r=>r.Name == "Stock keeper").Users.Any()) return;

            var keeper3 = new UserModel
            {
                Email = "keeper3@gmail.com",
                UserName = "keeper3@gmail.com",
                Department = toolContext.Departments.First(d => d.Name == "The workshop N3"),
                Stock = toolContext.Stocks.First(s=>s.Name == "Tool Stock N3")
            };
            var keeper6 = new UserModel
            {
                Email = "keeper6@gmail.com",
                UserName = "keeper6@gmail.com",
                Department = toolContext.Departments.First(d => d.Name == "The workshop N6"),
                Stock = toolContext.Stocks.First(s => s.Name == "Tool Stock N6")
            };

            var keeperUserInfo3 = new UserInfoModel
            {
                User = keeper3,
                Name = "keeper3",
                Surname = "keeper3",
                Email = keeper3.Email,
                Phone = "33-33"
            };
            var keeperUserInfo6 = new UserInfoModel
            {
                User = keeper6,
                Name = "keeper6",
                Surname = "keeper6",
                Email = keeper6.Email,
                Phone = "66-66"
            };

            var uManager = new UserManager<UserModel>(new UserStore<UserModel>(toolContext));
            var result3 = uManager.Create(keeper3, "Keeper3");
            var result6 = uManager.Create(keeper6, "Keeper6");

            toolContext.UserInfos.Add(keeperUserInfo3);
            toolContext.UserInfos.Add(keeperUserInfo6);

            if (result3.Succeeded)
            {
                // create role for default admin
                uManager.AddToRole(keeper3.Id, toolContext.Roles.FirstOrDefault(r => r.Name == "Stock keeper")?.Name);
            }
            if (result6.Succeeded)
            {
                // create role for default admin
                uManager.AddToRole(keeper6.Id, toolContext.Roles.FirstOrDefault(r => r.Name == "Stock keeper")?.Name);
            }
        }
    }
}
