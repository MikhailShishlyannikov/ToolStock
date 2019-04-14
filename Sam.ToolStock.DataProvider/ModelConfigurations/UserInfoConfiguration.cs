using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.ModelConfigurations
{
    public class UserInfoConfiguration : EntityTypeConfiguration<UserInfoModel>
    {
        public UserInfoConfiguration()
        {
            ToTable(ConfigEntityFramework.UserInfoTableName);
            
            Property(ui => ui.Name).IsRequired().HasMaxLength(ConfigEntityFramework.MaxLengthOfUserInfoName);

            Property(ui => ui.Patronymic).IsOptional()
                .HasMaxLength(ConfigEntityFramework.MaxLengthOfUserInfoPatronymic);

            Property(ui => ui.Surname).IsRequired().HasMaxLength(ConfigEntityFramework.MaxLengthOfUserInfoSurname);

            Property(ui => ui.Email).IsRequired().HasMaxLength(ConfigEntityFramework.MaxLengthOfUserInfoEmail);

            Property(ui => ui.Phone).IsOptional().HasMaxLength(ConfigEntityFramework.MaxLengthOfUserInfoPhone);
        }
    }
}
