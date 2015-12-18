namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KeyValues",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 40),
                        Value = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KeyValues");
        }
    }
}
