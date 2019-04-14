using System.Data.Entity.ModelConfiguration;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.ModelConfigurations
{
    public class ToolLogConfiguration : EntityTypeConfiguration<ToolLogModel>
    {
        public ToolLogConfiguration()
        {
            ToTable(ConfigEntityFramework.ToolLogTableName);

            Property(tl => tl.Date).IsRequired();
            Property(tl => tl.Status).IsRequired();
        }
    }
}
