namespace WebApplication_Parkovka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yangi2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Joylar", "Holati", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Joylar", "Holati");
        }
    }
}
