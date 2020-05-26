namespace SGE.Cagravol.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationForCustomerAndFileNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "Registered", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Customer", "Reserved", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.File", "FileNotes", c => c.String(defaultValue: ""));
            AddColumn("dbo.FileUpload", "FileNotes", c => c.String(defaultValue: ""));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileUpload", "FileNotes");
            DropColumn("dbo.File", "FileNotes");
            DropColumn("dbo.Customer", "Reserved");
            DropColumn("dbo.Customer", "Registered");
        }
    }
}
