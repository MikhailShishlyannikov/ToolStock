using System;
using System.Collections.Generic;
using System.Linq;
using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.Initializers
{
    public static class ToolTypeInitializer
    {
        public static void Initialize(ToolContext toolContext)
        {
            if (toolContext.ToolTypes.Any()) return;

            var toolTypes = new List<ToolTypeModel>
            {
                new ToolTypeModel {Id = Guid.NewGuid().ToString(), Name = "Shrinkfit holders"},
                new ToolTypeModel {Id = Guid.NewGuid().ToString(), Name = "Sharp solid end mills"},
                new ToolTypeModel {Id = Guid.NewGuid().ToString(), Name = "Ball-nose solid end mills"},
                new ToolTypeModel {Id = Guid.NewGuid().ToString(), Name = "Solid end mills with a corner radius"},
                new ToolTypeModel {Id = Guid.NewGuid().ToString(), Name = "Solid end mills with a concave radius"},
                new ToolTypeModel {Id = Guid.NewGuid().ToString(), Name = "Solid end mills with a chamfer"},
                new ToolTypeModel {Id = Guid.NewGuid().ToString(), Name = "T-shape solid end mills"},
                new ToolTypeModel {Id = Guid.NewGuid().ToString(), Name = "High precision collet chucks"},
                new ToolTypeModel {Id = Guid.NewGuid().ToString(), Name = "Hydraulic chucks"},
                new ToolTypeModel {Id = Guid.NewGuid().ToString(), Name = "Universal drill chucks "},
                new ToolTypeModel {Id = Guid.NewGuid().ToString(), Name = "Drills"}
            };

            toolContext.ToolTypes.AddRange(toolTypes);
            toolContext.SaveChanges();
        }
    }
}
