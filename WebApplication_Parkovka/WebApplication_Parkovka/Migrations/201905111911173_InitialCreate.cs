namespace WebApplication_Parkovka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JadvalFoy",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoyFISH = c.String(),
                        MoshinaN = c.String(),
                        KelishV = c.DateTime(),
                        KetishV = c.DateTime(),
                        JoylarId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Joylar", t => t.JoylarId)
                .Index(t => t.JoylarId);
            
            CreateTable(
                "dbo.Joylar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nomeri = c.Int(nullable: false),
                        NarxId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Narx", t => t.NarxId)
                .Index(t => t.NarxId);
            
            CreateTable(
                "dbo.Narx",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Summa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Xodim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FISH = c.String(),
                        MoshinaR = c.String(),
                        UserlarId = c.Int(),
                        JoylarId = c.Int(),
                        Holat = c.Int(),
                        JadvalFoy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JadvalFoy", t => t.JadvalFoy_Id)
                .ForeignKey("dbo.Userlar", t => t.UserlarId)
                .ForeignKey("dbo.Joylar", t => t.JoylarId)
                .Index(t => t.UserlarId)
                .Index(t => t.JoylarId)
                .Index(t => t.JadvalFoy_Id);
            
            CreateTable(
                "dbo.JadvalXodim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        XodimId = c.Int(),
                        KelishV = c.DateTime(),
                        KetishV = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Xodim", t => t.XodimId)
                .Index(t => t.XodimId);
            
            CreateTable(
                "dbo.Userlar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Parol = c.String(),
                        RollarId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rollar", t => t.RollarId)
                .Index(t => t.RollarId);
            
            CreateTable(
                "dbo.Rollar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nomi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Xodim", "JoylarId", "dbo.Joylar");
            DropForeignKey("dbo.Xodim", "UserlarId", "dbo.Userlar");
            DropForeignKey("dbo.Userlar", "RollarId", "dbo.Rollar");
            DropForeignKey("dbo.JadvalXodim", "XodimId", "dbo.Xodim");
            DropForeignKey("dbo.Xodim", "JadvalFoy_Id", "dbo.JadvalFoy");
            DropForeignKey("dbo.Joylar", "NarxId", "dbo.Narx");
            DropForeignKey("dbo.JadvalFoy", "JoylarId", "dbo.Joylar");
            DropIndex("dbo.Userlar", new[] { "RollarId" });
            DropIndex("dbo.JadvalXodim", new[] { "XodimId" });
            DropIndex("dbo.Xodim", new[] { "JadvalFoy_Id" });
            DropIndex("dbo.Xodim", new[] { "JoylarId" });
            DropIndex("dbo.Xodim", new[] { "UserlarId" });
            DropIndex("dbo.Joylar", new[] { "NarxId" });
            DropIndex("dbo.JadvalFoy", new[] { "JoylarId" });
            DropTable("dbo.Rollar");
            DropTable("dbo.Userlar");
            DropTable("dbo.JadvalXodim");
            DropTable("dbo.Xodim");
            DropTable("dbo.Narx");
            DropTable("dbo.Joylar");
            DropTable("dbo.JadvalFoy");
        }
    }
}
