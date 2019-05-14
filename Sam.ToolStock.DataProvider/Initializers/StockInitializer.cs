using System;
using System.Collections.Generic;
using System.Linq;
using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.Initializers
{
    public static class StockInitializer
    {
        public static void Initialize(ToolContext toolContext)
        {
            if(toolContext.Stocks.Any()) return;

            var stocks = new List<StockModel>
            {
                new StockModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Tool Stock N3",
                    Department = toolContext.Departments.First(d => d.Name == "The workshop N3")
                },
                new StockModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Tool Stock N6",
                    Department = toolContext.Departments.First(d=>d.Name == "The workshop N6")
                },
                new StockModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Tool Stock N4",
                    Department = toolContext.Departments.First(t=>t.Name == "The workshop N4")
                },
                new StockModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Central tool stock",
                    Department = toolContext.Departments.First(t=>t.Name == "The tool-die management")
                }
            };

            toolContext.Stocks.AddRange(stocks);

            toolContext.SaveChanges();
        }
    }
}
