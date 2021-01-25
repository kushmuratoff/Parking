namespace WebApplication_Parkovka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yangi1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Xodim", "JadvalFoy_Id", "dbo.JadvalFoy");
            DropIndex("dbo.Xodim", new[] { "JadvalFoy_Id" });
            DropColumn("dbo.Xodim", "JadvalFoy_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Xodim", "JadvalFoy_Id", c => c.Int());
            CreateIndex("dbo.Xodim", "JadvalFoy_Id");
            AddForeignKey("dbo.Xodim", "JadvalFoy_Id", "dbo.JadvalFoy", "Id");
        }
    }
}
