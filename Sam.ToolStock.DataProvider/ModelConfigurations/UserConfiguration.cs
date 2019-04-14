using System.Data.Entity.ModelConfiguration;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.ModelConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<UserModel>
    {
        public UserConfiguration()
        {
            ToTable(ConfigEntityFramework.UserTableName);

            HasOptional(u => u.UserInfo)
                .WithRequired(ui => ui.User);
        }
    }
}
