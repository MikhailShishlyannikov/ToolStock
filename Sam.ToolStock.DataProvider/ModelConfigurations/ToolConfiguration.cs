using System.Data.Entity.ModelConfiguration;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.ModelConfigurations
{
    public class ToolConfiguration : EntityTypeConfiguration<ToolModel>
    {
        public ToolConfiguration()
        {
            ToTable(ConfigEntityFramework.ToolTableName);

            Property(t => t.Name).IsRequired().HasMaxLength(ConfigEntityFramework.MaxLengthOfToolName);
            Property(t => t.Manufacturer).IsOptional().HasMaxLength(ConfigEntityFramework.MaxLengthOfToolManufacturer);
            Property(t => t.Status).IsRequired();
        }
    }
}
