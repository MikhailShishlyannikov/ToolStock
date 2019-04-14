using System;
using System.Collections.Generic;
using System.Linq;
using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.Initializers
{
    public static class DepartmentInitializer
    {
        public static void Initialize(ToolContext toolContext)
        {
            if (toolContext.Departments.Any()) return;

            var departments = new List<DepartmentModel>
            {
                new DepartmentModel {Id = Guid.NewGuid().ToString() ,Name = "The tool-die management"},
                new DepartmentModel {Id = Guid.NewGuid().ToString() ,Name = "The experimental workshop"},
                new DepartmentModel {Id = Guid.NewGuid().ToString() ,Name = "The flexible manufacturing systems workshop"},
                new DepartmentModel {Id = Guid.NewGuid().ToString() ,Name = "The workshop N3"},
                new DepartmentModel {Id = Guid.NewGuid().ToString() ,Name = "The workshop N6"},
                new DepartmentModel {Id = Guid.NewGuid().ToString() ,Name = "The workshop N4"}
            };

            toolContext.Departments.AddRange(departments);
            toolContext.SaveChanges();
        }
    }
}
