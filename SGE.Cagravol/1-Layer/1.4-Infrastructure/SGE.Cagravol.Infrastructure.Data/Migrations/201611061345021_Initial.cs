namespace SGE.Cagravol.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppConfiguration",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Key = c.String(maxLength: 1024),
                        Value = c.String(),
                        FieldType = c.Int(nullable: false),
                        IsPublicViewable = c.Boolean(nullable: false),
                        IsPublicEnable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BillDataType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1024),
                        I18NCode = c.String(maxLength: 1024),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProjectId = c.Long(nullable: false),
                        BillDataTypeId = c.Long(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Name = c.String(maxLength: 1024),
                        Notes = c.String(),
                        IsGeneric = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 1024),
                        PasswordHash = c.String(maxLength: 2048),
                        SignUpCode = c.String(maxLength: 1024),
                        BillLegalIdentification = c.String(),
                        BillAddress = c.String(),
                        BillPostalCode = c.String(maxLength: 64),
                        BillCity = c.String(maxLength: 1024),
                        BillCountry = c.String(maxLength: 1024),
                        SpaceNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillDataType", t => t.BillDataTypeId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.BillDataTypeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.File",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CustomerId = c.Long(nullable: false),
                        FileTypeId = c.Long(nullable: false),
                        Name = c.String(maxLength: 1024),
                        URL = c.String(),
                        ChannelId = c.String(maxLength: 1024),
                        Version = c.Int(nullable: false),
                        MimeType = c.String(maxLength: 1024),
                        FileName = c.String(maxLength: 1024),
                        Size = c.Long(nullable: false),
                        FirstDeliveryDate = c.DateTime(nullable: false),
                        WFWorkflowId = c.Long(),
                        WFWorkflowVersion = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileType", t => t.FileTypeId)
                .ForeignKey("dbo.WFWorkflow", t => t.WFWorkflowId)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.FileTypeId)
                .Index(t => t.WFWorkflowId);
            
            CreateTable(
                "dbo.FileType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Notes = c.String(),
                        FileExtension = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WFFileState",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EntityId = c.Long(nullable: false),
                        WFStateId = c.Long(nullable: false),
                        UserId = c.String(maxLength: 128),
                        TS = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.WFState", t => t.WFStateId, cascadeDelete: true)
                .ForeignKey("dbo.File", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId)
                .Index(t => t.WFStateId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WFFileStateNote",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WFEntityStateId = c.Long(nullable: false),
                        UserId = c.String(maxLength: 128),
                        TS = c.DateTime(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.WFFileState", t => t.WFEntityStateId, cascadeDelete: true)
                .Index(t => t.WFEntityStateId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 1024),
                        Surname = c.String(maxLength: 1024),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        LastAccess = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 1024),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 1024),
                        SecurityStamp = c.String(maxLength: 1024),
                        PhoneNumber = c.String(maxLength: 1024),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProviderKey, t.LoginProvider })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.IdentityRole_Id)
                .Index(t => t.UserId)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.UserProject",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProjectId = c.Long(nullable: false),
                        UserId = c.String(maxLength: 128),
                        IsAuthorized = c.Boolean(nullable: false),
                        IsOwner = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1024),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        FinishDate = c.DateTime(nullable: false),
                        ExtraChargeForSendingDate = c.DateTime(nullable: false),
                        LimitForSendingDate = c.DateTime(nullable: false),
                        ExtraChargePercentage = c.Double(nullable: false),
                        Notes = c.String(),
                        TotalStands = c.Long(nullable: false),
                        Code = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WFState",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WFSecurityPresetGroupId = c.Long(),
                        WFGrantedGroupId = c.Long(),
                        WFDeniedGroupId = c.Long(),
                        Code = c.String(maxLength: 1024),
                        Name = c.String(maxLength: 1024),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WFGroup", t => t.WFDeniedGroupId)
                .ForeignKey("dbo.WFGroup", t => t.WFGrantedGroupId)
                .ForeignKey("dbo.WFSecurityPresetGroup", t => t.WFSecurityPresetGroupId)
                .Index(t => t.WFSecurityPresetGroupId)
                .Index(t => t.WFGrantedGroupId)
                .Index(t => t.WFDeniedGroupId);
            
            CreateTable(
                "dbo.WFTransition",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WFNotificationGroupId = c.Long(),
                        WFNotificationPresetGroupId = c.Long(),
                        WFDefaultStateOriginId = c.Long(nullable: false),
                        WFDefaultStateDestinationId = c.Long(nullable: false),
                        CouldComment = c.Boolean(nullable: false),
                        MustComment = c.Boolean(nullable: false),
                        Code = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WFGroup", t => t.WFNotificationGroupId)
                .ForeignKey("dbo.WFNotifyPresetGroup", t => t.WFNotificationPresetGroupId)
                .ForeignKey("dbo.WFState", t => t.WFDefaultStateDestinationId)
                .ForeignKey("dbo.WFState", t => t.WFDefaultStateOriginId)
                .Index(t => t.WFNotificationGroupId)
                .Index(t => t.WFNotificationPresetGroupId)
                .Index(t => t.WFDefaultStateOriginId)
                .Index(t => t.WFDefaultStateDestinationId);
            
            CreateTable(
                "dbo.WFGroup",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1024),
                        IsPreset = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WFRole",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoleId = c.String(maxLength: 128),
                        WFGroupId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.RoleId)
                .ForeignKey("dbo.WFGroup", t => t.WFGroupId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.WFGroupId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 4000),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WFUser",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        WFGroupId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.WFGroup", t => t.WFGroupId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.WFGroupId);
            
            CreateTable(
                "dbo.WFNotifyPresetGroup",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1024),
                        WFNotificationGroupId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WFGroup", t => t.WFNotificationGroupId)
                .Index(t => t.WFNotificationGroupId);
            
            CreateTable(
                "dbo.WFTransitionProcess",
                c => new
                    {
                        WFTransitionId = c.Long(nullable: false),
                        WFProcessId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.WFTransitionId, t.WFProcessId })
                .ForeignKey("dbo.WFProcess", t => t.WFProcessId, cascadeDelete: true)
                .ForeignKey("dbo.WFTransition", t => t.WFTransitionId)
                .Index(t => t.WFTransitionId)
                .Index(t => t.WFProcessId);
            
            CreateTable(
                "dbo.WFProcess",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1024),
                        Description = c.String(),
                        Code = c.String(maxLength: 1024),
                        EntityType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WFTransitionRequirement",
                c => new
                    {
                        WFTransitionId = c.Long(nullable: false),
                        WFRequirementId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.WFTransitionId, t.WFRequirementId })
                .ForeignKey("dbo.WFRequirement", t => t.WFRequirementId, cascadeDelete: true)
                .ForeignKey("dbo.WFTransition", t => t.WFTransitionId)
                .Index(t => t.WFTransitionId)
                .Index(t => t.WFRequirementId);
            
            CreateTable(
                "dbo.WFRequirement",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1024),
                        Description = c.String(),
                        Code = c.String(maxLength: 1024),
                        EntityType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WFWorkflowTransition",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WFTransitionId = c.Long(nullable: false),
                        WFWorkflowId = c.Long(nullable: false),
                        WFWorkflowVersion = c.Long(nullable: false),
                        WFWorkflowStateOriginId = c.Long(nullable: false),
                        WFWorkflowStateDestinationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WFWorkflowState", t => t.WFWorkflowStateDestinationId)
                .ForeignKey("dbo.WFWorkflowState", t => t.WFWorkflowStateOriginId)
                .ForeignKey("dbo.WFWorkflow", t => t.WFWorkflowId, cascadeDelete: true)
                .ForeignKey("dbo.WFTransition", t => t.WFTransitionId)
                .Index(t => t.WFTransitionId)
                .Index(t => t.WFWorkflowId)
                .Index(t => t.WFWorkflowStateOriginId)
                .Index(t => t.WFWorkflowStateDestinationId);
            
            CreateTable(
                "dbo.WFWorkflow",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WFWorkflowVersion = c.Long(nullable: false),
                        Name = c.String(maxLength: 1024),
                        Code = c.String(maxLength: 1024),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WFWorkflowState",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WFStateId = c.Long(nullable: false),
                        WFWorkflowId = c.Long(nullable: false),
                        WFWorkflowVersion = c.Long(nullable: false),
                        IsInitial = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WFWorkflow", t => t.WFWorkflowId, cascadeDelete: true)
                .ForeignKey("dbo.WFState", t => t.WFStateId)
                .Index(t => t.WFStateId)
                .Index(t => t.WFWorkflowId);
            
            CreateTable(
                "dbo.WFWorkflowRelatedEntity",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WFWorkflowId = c.Long(nullable: false),
                        Name = c.String(maxLength: 1024),
                        EntityType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WFWorkflow", t => t.WFWorkflowId, cascadeDelete: true)
                .Index(t => t.WFWorkflowId);
            
            CreateTable(
                "dbo.WFSecurityPresetGroup",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1024),
                        WFGrantedGroupId = c.Long(),
                        WFDeniedGroupId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WFGroup", t => t.WFDeniedGroupId)
                .ForeignKey("dbo.WFGroup", t => t.WFGrantedGroupId)
                .Index(t => t.WFGrantedGroupId)
                .Index(t => t.WFDeniedGroupId);
            
            CreateTable(
                "dbo.FileUpload",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CustomerId = c.Long(nullable: false),
                        MimeType = c.String(maxLength: 1024),
                        Name = c.String(maxLength: 1024),
                        CustomerLogicalName = c.String(maxLength: 1024),
                        ChannelId = c.String(maxLength: 1024),
                        PartsCounter = c.Long(nullable: false),
                        PartsTotal = c.Long(nullable: false),
                        Size = c.Long(nullable: false),
                        TempFolder = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        LastHashUploadDate = c.DateTime(nullable: false),
                        UploadPartsMapCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "IdentityRole_Id", "dbo.Role");
            DropForeignKey("dbo.FileUpload", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.File", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.File", "WFWorkflowId", "dbo.WFWorkflow");
            DropForeignKey("dbo.WFFileState", "EntityId", "dbo.File");
            DropForeignKey("dbo.WFFileState", "WFStateId", "dbo.WFState");
            DropForeignKey("dbo.WFWorkflowState", "WFStateId", "dbo.WFState");
            DropForeignKey("dbo.WFState", "WFSecurityPresetGroupId", "dbo.WFSecurityPresetGroup");
            DropForeignKey("dbo.WFSecurityPresetGroup", "WFGrantedGroupId", "dbo.WFGroup");
            DropForeignKey("dbo.WFSecurityPresetGroup", "WFDeniedGroupId", "dbo.WFGroup");
            DropForeignKey("dbo.WFState", "WFGrantedGroupId", "dbo.WFGroup");
            DropForeignKey("dbo.WFState", "WFDeniedGroupId", "dbo.WFGroup");
            DropForeignKey("dbo.WFTransition", "WFDefaultStateOriginId", "dbo.WFState");
            DropForeignKey("dbo.WFTransition", "WFDefaultStateDestinationId", "dbo.WFState");
            DropForeignKey("dbo.WFWorkflowTransition", "WFTransitionId", "dbo.WFTransition");
            DropForeignKey("dbo.WFWorkflowRelatedEntity", "WFWorkflowId", "dbo.WFWorkflow");
            DropForeignKey("dbo.WFWorkflowTransition", "WFWorkflowId", "dbo.WFWorkflow");
            DropForeignKey("dbo.WFWorkflowState", "WFWorkflowId", "dbo.WFWorkflow");
            DropForeignKey("dbo.WFWorkflowTransition", "WFWorkflowStateOriginId", "dbo.WFWorkflowState");
            DropForeignKey("dbo.WFWorkflowTransition", "WFWorkflowStateDestinationId", "dbo.WFWorkflowState");
            DropForeignKey("dbo.WFTransitionRequirement", "WFTransitionId", "dbo.WFTransition");
            DropForeignKey("dbo.WFTransitionRequirement", "WFRequirementId", "dbo.WFRequirement");
            DropForeignKey("dbo.WFTransitionProcess", "WFTransitionId", "dbo.WFTransition");
            DropForeignKey("dbo.WFTransitionProcess", "WFProcessId", "dbo.WFProcess");
            DropForeignKey("dbo.WFTransition", "WFNotificationPresetGroupId", "dbo.WFNotifyPresetGroup");
            DropForeignKey("dbo.WFNotifyPresetGroup", "WFNotificationGroupId", "dbo.WFGroup");
            DropForeignKey("dbo.WFTransition", "WFNotificationGroupId", "dbo.WFGroup");
            DropForeignKey("dbo.WFUser", "WFGroupId", "dbo.WFGroup");
            DropForeignKey("dbo.WFUser", "UserId", "dbo.User");
            DropForeignKey("dbo.WFRole", "WFGroupId", "dbo.WFGroup");
            DropForeignKey("dbo.WFRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.WFFileState", "UserId", "dbo.User");
            DropForeignKey("dbo.WFFileStateNote", "WFEntityStateId", "dbo.WFFileState");
            DropForeignKey("dbo.WFFileStateNote", "UserId", "dbo.User");
            DropForeignKey("dbo.UserProject", "UserId", "dbo.User");
            DropForeignKey("dbo.UserProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Customer", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.Customer", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.File", "FileTypeId", "dbo.FileType");
            DropForeignKey("dbo.Customer", "BillDataTypeId", "dbo.BillDataType");
            DropIndex("dbo.FileUpload", new[] { "CustomerId" });
            DropIndex("dbo.WFSecurityPresetGroup", new[] { "WFDeniedGroupId" });
            DropIndex("dbo.WFSecurityPresetGroup", new[] { "WFGrantedGroupId" });
            DropIndex("dbo.WFWorkflowRelatedEntity", new[] { "WFWorkflowId" });
            DropIndex("dbo.WFWorkflowState", new[] { "WFWorkflowId" });
            DropIndex("dbo.WFWorkflowState", new[] { "WFStateId" });
            DropIndex("dbo.WFWorkflowTransition", new[] { "WFWorkflowStateDestinationId" });
            DropIndex("dbo.WFWorkflowTransition", new[] { "WFWorkflowStateOriginId" });
            DropIndex("dbo.WFWorkflowTransition", new[] { "WFWorkflowId" });
            DropIndex("dbo.WFWorkflowTransition", new[] { "WFTransitionId" });
            DropIndex("dbo.WFTransitionRequirement", new[] { "WFRequirementId" });
            DropIndex("dbo.WFTransitionRequirement", new[] { "WFTransitionId" });
            DropIndex("dbo.WFTransitionProcess", new[] { "WFProcessId" });
            DropIndex("dbo.WFTransitionProcess", new[] { "WFTransitionId" });
            DropIndex("dbo.WFNotifyPresetGroup", new[] { "WFNotificationGroupId" });
            DropIndex("dbo.WFUser", new[] { "WFGroupId" });
            DropIndex("dbo.WFUser", new[] { "UserId" });
            DropIndex("dbo.WFRole", new[] { "WFGroupId" });
            DropIndex("dbo.WFRole", new[] { "RoleId" });
            DropIndex("dbo.WFTransition", new[] { "WFDefaultStateDestinationId" });
            DropIndex("dbo.WFTransition", new[] { "WFDefaultStateOriginId" });
            DropIndex("dbo.WFTransition", new[] { "WFNotificationPresetGroupId" });
            DropIndex("dbo.WFTransition", new[] { "WFNotificationGroupId" });
            DropIndex("dbo.WFState", new[] { "WFDeniedGroupId" });
            DropIndex("dbo.WFState", new[] { "WFGrantedGroupId" });
            DropIndex("dbo.WFState", new[] { "WFSecurityPresetGroupId" });
            DropIndex("dbo.UserProject", new[] { "UserId" });
            DropIndex("dbo.UserProject", new[] { "ProjectId" });
            DropIndex("dbo.UserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.WFFileStateNote", new[] { "UserId" });
            DropIndex("dbo.WFFileStateNote", new[] { "WFEntityStateId" });
            DropIndex("dbo.WFFileState", new[] { "UserId" });
            DropIndex("dbo.WFFileState", new[] { "WFStateId" });
            DropIndex("dbo.WFFileState", new[] { "EntityId" });
            DropIndex("dbo.File", new[] { "WFWorkflowId" });
            DropIndex("dbo.File", new[] { "FileTypeId" });
            DropIndex("dbo.File", new[] { "CustomerId" });
            DropIndex("dbo.Customer", new[] { "UserId" });
            DropIndex("dbo.Customer", new[] { "BillDataTypeId" });
            DropIndex("dbo.Customer", new[] { "ProjectId" });
            DropTable("dbo.Log");
            DropTable("dbo.FileUpload");
            DropTable("dbo.WFSecurityPresetGroup");
            DropTable("dbo.WFWorkflowRelatedEntity");
            DropTable("dbo.WFWorkflowState");
            DropTable("dbo.WFWorkflow");
            DropTable("dbo.WFWorkflowTransition");
            DropTable("dbo.WFRequirement");
            DropTable("dbo.WFTransitionRequirement");
            DropTable("dbo.WFProcess");
            DropTable("dbo.WFTransitionProcess");
            DropTable("dbo.WFNotifyPresetGroup");
            DropTable("dbo.WFUser");
            DropTable("dbo.Role");
            DropTable("dbo.WFRole");
            DropTable("dbo.WFGroup");
            DropTable("dbo.WFTransition");
            DropTable("dbo.WFState");
            DropTable("dbo.Project");
            DropTable("dbo.UserProject");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.WFFileStateNote");
            DropTable("dbo.WFFileState");
            DropTable("dbo.FileType");
            DropTable("dbo.File");
            DropTable("dbo.Customer");
            DropTable("dbo.BillDataType");
            DropTable("dbo.AppConfiguration");
        }
    }
}
