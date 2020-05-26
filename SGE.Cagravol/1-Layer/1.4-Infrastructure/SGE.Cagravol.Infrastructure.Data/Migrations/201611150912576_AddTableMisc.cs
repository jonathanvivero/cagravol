namespace SGE.Cagravol.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableMisc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Misc",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Key = c.String(maxLength: 1024),
                        Value = c.String(),
                        Limit = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Misc");
        }
    }
}
