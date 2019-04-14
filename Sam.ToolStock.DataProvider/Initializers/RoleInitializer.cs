using System.Collections.Generic;
using System.Linq;
using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.Initializers
{
    public static class RoleInitializer
    {
        public static void Initialize(ToolContext toolContext)
        {
            if(toolContext.Roles.Any()) return;

            var roles = new List<RoleModel>
            {
                new RoleModel() { Name = "Admin"},
                new RoleModel() { Name = "Stock keeper"},
                new RoleModel() { Name = "User"}
            };

            foreach (var role in roles)
            {
                toolContext.Roles.Add(role);
            }

            toolContext.SaveChanges();
        }
    }
}
