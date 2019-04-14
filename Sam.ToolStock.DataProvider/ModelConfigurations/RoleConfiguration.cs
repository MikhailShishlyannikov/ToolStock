using System.Data.Entity.ModelConfiguration;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.ModelConfigurations
{
    public class RoleConfiguration : EntityTypeConfiguration<RoleModel>
    {
        public RoleConfiguration()
        {
            ToTable(ConfigEntityFramework.RoleTableName);
        }
    }
}
