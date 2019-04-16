using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Initializers;

namespace Sam.ToolStock.DataProvider.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Contexts.ToolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contexts.ToolContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            DepartmentInitializer.Initialize(context);
            RoleInitializer.Initialize(context);
            StockInitializer.Initialize(context);
            AdminInitializer.Initialize(context);
            StockKeeperInitializer.Initialize(context);
            UserInitializer.Initialize(context);
            context.SaveChanges();
        }
    }
}
