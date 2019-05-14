using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sam.ToolStock.Common;
using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.Initializers
{
    public static class ToolInitializer
    {
        public static void Initialize(ToolContext toolContext)
        {
            if (toolContext.Tools.Any()) return;

            var rManager = new RoleManager<RoleModel>(new RoleStore<RoleModel>(toolContext));

            var tools = new List<ToolModel>
            {
                new ToolModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "JS564030F2C.0Z4C-NXT",
                    Manufacturer = "SECO",
                    Status = Statuses.InStock,
                    ToolTypeId = toolContext.ToolTypes.First(tt => tt.Name == "Sharp solid end mills").Id,
                    StockId = toolContext.Stocks.First(s => s.Name == "Central tool stock").Id,
                    UserId = rManager.FindByName("Admin").Users.First().UserId
                },
                new ToolModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "JME542015G1R015.0Z2-SIRA",
                    Manufacturer = "SECO",
                    Status = Statuses.InStock,
                    ToolTypeId = toolContext.ToolTypes.First(tt => tt.Name == "Solid end mills with a corner radius").Id,
                    StockId = toolContext.Stocks.First(s => s.Name == "Central tool stock").Id,
                    UserId = rManager.FindByName("Admin").Users.First().UserId
                },
                new ToolModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "970050-TRIBON",
                    Manufacturer = "SECO",
                    Status = Statuses.InStock,
                    ToolTypeId = toolContext.ToolTypes.First(tt => tt.Name == "Ball-nose solid end mills").Id,
                    StockId = toolContext.Stocks.First(s => s.Name == "Central tool stock").Id,
                    UserId = rManager.FindByName("Admin").Users.First().UserId
                },
                new ToolModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "JS720200D3R100.0Z6-HXT",
                    Manufacturer = "SECO",
                    Status = Statuses.InStock,
                    ToolTypeId = toolContext.ToolTypes.First(tt => tt.Name == "Solid end mills with a corner radius").Id,
                    StockId = toolContext.Stocks.First(s => s.Name == "Central tool stock").Id,
                    UserId = rManager.FindByName("Admin").Users.First().UserId
                },
                new ToolModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "E321456001270",
                    Manufacturer = "SECO",
                    Status = Statuses.InStock,
                    ToolTypeId = toolContext.ToolTypes.First(tt => tt.Name == "Shrinkfit holders").Id,
                    StockId = toolContext.Stocks.First(s => s.Name == "Central tool stock").Id,
                    UserId = rManager.FindByName("Admin").Users.First().UserId
                },
                new ToolModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "E3416508516",
                    Manufacturer = "SECO",
                    Status = Statuses.InStock,
                    ToolTypeId = toolContext.ToolTypes.First(tt => tt.Name == "Universal drill chucks").Id,
                    StockId = toolContext.Stocks.First(s => s.Name == "Central tool stock").Id,
                    UserId = rManager.FindByName("Admin").Users.First().UserId
                },
                new ToolModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "3839-12.000",
                    Manufacturer = "Guhring",
                    Status = Statuses.InStock,
                    ToolTypeId = toolContext.ToolTypes.First(tt => tt.Name == "Sharp solid end mills").Id,
                    StockId = toolContext.Stocks.First(s => s.Name == "Central tool stock").Id,
                    UserId = rManager.FindByName("Admin").Users.First().UserId
                },
                new ToolModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "5510-3.500",
                    Manufacturer = "Guhring",
                    Status = Statuses.InStock,
                    ToolTypeId = toolContext.ToolTypes.First(tt => tt.Name == "Drills").Id,
                    StockId = toolContext.Stocks.First(s => s.Name == "Central tool stock").Id,
                    UserId = rManager.FindByName("Admin").Users.First().UserId
                }
            };

            foreach (var tool in tools)
            {
                tool.ToolLogs = new List<ToolLogModel>
                {
                    new ToolLogModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Status = tool.Status,
                        ToolId = tool.Id,
                        StockId = tool.StockId,
                        Date = DateTime.Now,
                        UserId = tool.UserId
                    }
                };
            }

            toolContext.Tools.AddRange(tools);
            toolContext.SaveChanges();
        }
    }
}
