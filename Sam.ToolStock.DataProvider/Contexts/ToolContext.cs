using System.Data.Entity;
using System.Reflection;
using Microsoft.AspNet.Identity.EntityFramework;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.DataProvider.Migrations;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.DataProvider.Contexts
{
    public class ToolContext : IdentityDbContext<UserModel>
    {
        public ToolContext() : base(ConfigEntityFramework.ConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ToolContext, Configuration>());
        }

        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<StockModel> Stocks { get; set; }
        public DbSet<ToolModel> Tools { get; set; }
        public DbSet<ToolLogModel> ToolLogs { get; set; }
        public DbSet<ToolTypeModel> ToolTypes { get; set; }
        public DbSet<UserInfoModel> UserInfos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable(ConfigEntityFramework.IdentityUserLoginTableName);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
