using System.Data.Entity.ModelConfiguration;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.ModelConfigurations
{
    public class StockConfiguration : EntityTypeConfiguration<StockModel>
    {
        public StockConfiguration()
        {
            ToTable(ConfigEntityFramework.StockTableName);

            Property(s => s.Name).IsRequired().HasMaxLength(ConfigEntityFramework.MaxLengthOfStockName);
        }
    }
}
