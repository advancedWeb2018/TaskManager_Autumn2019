namespace MakeIt.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeItDBv3ExceptionDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExceptionMessage = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        StackTrace = c.String(),
                        Date = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExceptionDetail");
        }
    }
}
