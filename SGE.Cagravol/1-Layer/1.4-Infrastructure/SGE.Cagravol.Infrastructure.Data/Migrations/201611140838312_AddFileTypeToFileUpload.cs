namespace SGE.Cagravol.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileTypeToFileUpload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileUpload", "FileTypeId", c => c.Long(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileUpload", "FileTypeId");
        }
    }
}
