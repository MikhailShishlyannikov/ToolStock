namespace Sam.ToolStock.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seeding : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 350),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 70),
                        IsDeleted = c.Boolean(nullable: false),
                        DepartmentId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.ToolLogs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Status = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ToolId = c.String(maxLength: 128),
                        StockId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .ForeignKey("dbo.Tools", t => t.ToolId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ToolId)
                .Index(t => t.StockId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tools",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 60),
                        Manufacturer = c.String(maxLength: 60),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ToolTypeId = c.String(maxLength: 128),
                        StockId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .ForeignKey("dbo.ToolTypes", t => t.ToolTypeId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ToolTypeId)
                .Index(t => t.StockId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ToolTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        DepartmentId = c.String(maxLength: 128),
                        StockId = c.String(maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .Index(t => t.DepartmentId)
                .Index(t => t.StockId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        UserModel_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserModel_Id)
                .Index(t => t.UserModel_Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        UserModel_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserModel_Id)
                .Index(t => t.UserModel_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserModel_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserModel_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserModel_Id);
            
            CreateTable(
                "dbo.UserInfos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 60),
                        Patronymic = c.String(maxLength: 70),
                        Surname = c.String(nullable: false, maxLength: 80),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetRoles", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserInfos", "Id", "dbo.Users");
            DropForeignKey("dbo.Tools", "UserId", "dbo.Users");
            DropForeignKey("dbo.ToolLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.AspNetUserRoles", "UserModel_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserModel_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.AspNetUserClaims", "UserModel_Id", "dbo.Users");
            DropForeignKey("dbo.Tools", "ToolTypeId", "dbo.ToolTypes");
            DropForeignKey("dbo.ToolLogs", "ToolId", "dbo.Tools");
            DropForeignKey("dbo.Tools", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.ToolLogs", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Stocks", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Roles", new[] { "Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserInfos", new[] { "Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserModel_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserLogins", new[] { "UserModel_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserModel_Id" });
            DropIndex("dbo.Users", new[] { "StockId" });
            DropIndex("dbo.Users", new[] { "DepartmentId" });
            DropIndex("dbo.Tools", new[] { "UserId" });
            DropIndex("dbo.Tools", new[] { "StockId" });
            DropIndex("dbo.Tools", new[] { "ToolTypeId" });
            DropIndex("dbo.ToolLogs", new[] { "UserId" });
            DropIndex("dbo.ToolLogs", new[] { "StockId" });
            DropIndex("dbo.ToolLogs", new[] { "ToolId" });
            DropIndex("dbo.Stocks", new[] { "DepartmentId" });
            DropTable("dbo.Roles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UserInfos");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.ToolTypes");
            DropTable("dbo.Tools");
            DropTable("dbo.ToolLogs");
            DropTable("dbo.Stocks");
            DropTable("dbo.Departments");
        }
    }
}
