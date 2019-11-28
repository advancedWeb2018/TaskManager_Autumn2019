namespace MakeIt.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeItDBv2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Project", "IsPrivate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project", "IsPrivate", c => c.Boolean(nullable: false));
        }
    }
}
