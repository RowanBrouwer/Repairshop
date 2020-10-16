namespace Repairshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AmountPartsInStorages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AmountInStorage = c.Int(nullable: false),
                        Part_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.parts", t => t.Part_Id)
                .Index(t => t.Part_Id);
            
            CreateTable(
                "dbo.parts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        Type = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        StreetName = c.String(),
                        City = c.String(),
                        PostCode = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PartsNeededs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AmountNeeded = c.Int(nullable: false),
                        inStorage_Id = c.Int(),
                        PartNeeded_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AmountPartsInStorages", t => t.inStorage_Id)
                .ForeignKey("dbo.parts", t => t.PartNeeded_Id)
                .Index(t => t.inStorage_Id)
                .Index(t => t.PartNeeded_Id);
            
            CreateTable(
                "dbo.RepairGuys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Complaints = c.String(),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.RepairOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        status = c.Int(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        customer_Id = c.Int(),
                        parts_Id = c.Int(),
                        repairGuy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.customer_Id)
                .ForeignKey("dbo.PartsNeededs", t => t.parts_Id)
                .ForeignKey("dbo.RepairGuys", t => t.repairGuy_Id)
                .Index(t => t.customer_Id)
                .Index(t => t.parts_Id)
                .Index(t => t.repairGuy_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RepairOrders", "repairGuy_Id", "dbo.RepairGuys");
            DropForeignKey("dbo.RepairOrders", "parts_Id", "dbo.PartsNeededs");
            DropForeignKey("dbo.RepairOrders", "customer_Id", "dbo.Customers");
            DropForeignKey("dbo.RepairGuys", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PartsNeededs", "PartNeeded_Id", "dbo.parts");
            DropForeignKey("dbo.PartsNeededs", "inStorage_Id", "dbo.AmountPartsInStorages");
            DropForeignKey("dbo.Customers", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AmountPartsInStorages", "Part_Id", "dbo.parts");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RepairOrders", new[] { "repairGuy_Id" });
            DropIndex("dbo.RepairOrders", new[] { "parts_Id" });
            DropIndex("dbo.RepairOrders", new[] { "customer_Id" });
            DropIndex("dbo.RepairGuys", new[] { "user_Id" });
            DropIndex("dbo.PartsNeededs", new[] { "PartNeeded_Id" });
            DropIndex("dbo.PartsNeededs", new[] { "inStorage_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Customers", new[] { "user_Id" });
            DropIndex("dbo.AmountPartsInStorages", new[] { "Part_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RepairOrders");
            DropTable("dbo.RepairGuys");
            DropTable("dbo.PartsNeededs");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Customers");
            DropTable("dbo.parts");
            DropTable("dbo.AmountPartsInStorages");
        }
    }
}
