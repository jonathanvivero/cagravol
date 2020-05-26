using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Domain.Entities.Workflows;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Infrastructure.Data.DatabaseInitializers;
using SGE.Cagravol.Domain.Entities.History;

namespace SGE.Cagravol.Infrastructure.Data
{
    public class SGEContext: IdentityDbContext<User>, ISGEContext //, Role, string, UserLogin, UserRole, UserClaim>, ISGEContext
    {
        public DbSet<AppConfiguration> AppConfigurations { get; set; }
        public DbSet<BillDataType> BillDataTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<WFFileState> WFFileStates { get; set; }
        public DbSet<WFFileStateNote> WFFileStateNotes { get; set; }        
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<Misc> Miscellaneous { get; set; }
        public DbSet<WFWorkflow> WFWorkflows { get; set; }
        public DbSet<WFState> WFStates { get; set; }
        public DbSet<WFWorkflowTransition> WFWorkflowTransitions { get; set; }
        public DbSet<WFWorkflowState> WFWorkflowStates { get; set; }
        public DbSet<WFTransition> WFTransitions { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        public DbSet<WFGroup> WFGroups { get; set; }
        public DbSet<WFNotifyPresetGroup> WFNotifyPresetGroups { get; set; }
        public DbSet<WFProcess> WFProcesses { get; set; }
        public DbSet<WFRequirement> WFRequirements { get; set; }
        public DbSet<WFRole> WFRoles { get; set; }
        public DbSet<WFSecurityPresetGroup> WFSecurityPresetGroups { get; set; }
        public DbSet<WFTransitionProcess> WFTransitionProcesses { get; set; }
        public DbSet<WFTransitionRequirement> WFTransitionRequirements { get; set; }
        public DbSet<WFUser> WFUsers { get; set; }
        public DbSet<WFWorkflowRelatedEntity> WFWorkflowRelatedEntities { get; set; }
        public DbSet<Log> Logs { get; set; }
        

        ////Identity
        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }
        //public DbSet<UserLogin> UserLogins { get; set; }

       
        public static ISGEContext Create()
        {
            return new SGEContext();
        }

        public SGEContext()
            : base("SGE.Cagravol.Infrastructure.Data.SGEContext")
		{
            Database.SetInitializer(new AutoMigrationDatabaseInitializer<SGEContext>());
            Database.Initialize(false);
		}

        public Database CurrentDatabase { get { return this.Database; } }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(SGEContext).Assembly);

            // By default, all strings in original database are varchar.
            modelBuilder.Properties<string>()
                .Configure(config => config
                    .HasColumnType("nvarchar")
                    .IsMaxLength()
                );

            // By default, all longs in original database are bigint.
            modelBuilder.Properties<long>()
                .Configure(config => config
                    .HasColumnType("bigint")
                );            
            
            //This hack is to accept Two Foreign Keys for the same table, in cases: 
            //  Contract -> (Collection of <ContractStampationFrame>) AgentStampations)
            //  Contract -> (Collection of <ContractStampationFrame>) UserStampations)

        }
        public void SetState(object o, EntityState newState)
        {
            this.Entry(o).State = newState;
        }

        //DbSet<T> Set<T>() where T : class;
        //int SaveChanges();
        //Task<int> SaveChangesAsync();        
        //void Dispose();

    }
}
