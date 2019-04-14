using System.Data.Entity.ModelConfiguration;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.ModelConfigurations
{
    public class DepartmentConfiguration : EntityTypeConfiguration<DepartmentModel>
    {
        public DepartmentConfiguration()
        {
            ToTable(ConfigEntityFramework.DepartmentTableName);

            HasKey(d => d.Id);

            Property(d => d.Name).IsRequired().HasMaxLength(ConfigEntityFramework.MaxLengthOfDepartmentName);
        }
    }
}
