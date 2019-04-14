using System.Data.Entity.ModelConfiguration;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.ModelConfigurations
{
    public class ToolTypeConfiguration : EntityTypeConfiguration<ToolTypeModel>
    {
        public ToolTypeConfiguration()
        {
            ToTable(ConfigEntityFramework.ToolTypeTableName);

            Property(tt => tt.Name).IsRequired().HasMaxLength(ConfigEntityFramework.MaxLengthOToolTypeName);
        }
    }
}
