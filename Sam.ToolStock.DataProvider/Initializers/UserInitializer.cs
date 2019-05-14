using System;
using System.Linq;
using Bogus;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.Initializers
{
    public static class UserInitializer
    {
        public static void Initialize(ToolContext toolContext)
        {
            if (toolContext.Roles.First(r => r.Name == "User").Users.Any()) return;

            Randomizer.Seed = new Random(836662135);

            var departments = toolContext.Departments.ToList();

            var testUser = new Faker<UserModel>("en")
                .StrictMode(false)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.UserName, (f, u) => u.Email)
                .RuleFor(u => u.Department, f => f.PickRandom(departments));

            var testUserInfo = new Faker<UserInfoModel>("en")
                .StrictMode(false)
                .RuleFor(ui => ui.User, f => testUser.Generate())
                .RuleFor(ui => ui.Name, f => f.Name.FirstName())
                .RuleFor(ui => ui.Surname, f => f.Name.LastName())
                .RuleFor(ui => ui.Email, (f, ui) => ui.User.Email)
                .RuleFor(ui => ui.Phone, f => f.Phone.PhoneNumberFormat(0));

            var userInfos = testUserInfo.Generate(15);

            var uManager = new UserManager<UserModel>(new UserStore<UserModel>(toolContext));

            foreach (var userInfoModel in userInfos)
            {
                var result = uManager.Create(userInfoModel.User, "SimpleUser");
                if (result.Succeeded) uManager.AddToRole(userInfoModel.User.Id, "User");
            }

            toolContext.UserInfos.AddRange(userInfos);
        }
    }
}
