namespace MakeIt.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MakeItDBv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectUser",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.UserId })
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Project", "IsPrivate", c => c.Boolean(nullable: false));
            AddColumn("dbo.Project", "IsClosed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectUser", "UserId", "dbo.User");
            DropForeignKey("dbo.ProjectUser", "ProjectId", "dbo.Project");
            DropIndex("dbo.ProjectUser", new[] { "UserId" });
            DropIndex("dbo.ProjectUser", new[] { "ProjectId" });
            DropColumn("dbo.Project", "IsClosed");
            DropColumn("dbo.Project", "IsPrivate");
            DropTable("dbo.ProjectUser");
        }
    }
}
