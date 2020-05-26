using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Domain.Entities.Workflows;

namespace SGE.Cagravol.Infrastructure.Data
{
    public interface ISGEContext
    {
        DbSet<AppConfiguration> AppConfigurations { get; set; }
        DbSet<BillDataType> BillDataTypes { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<File> Files { get; set; }
        DbSet<FileUpload> FileUploads { get; set; }
        DbSet<WFFileState> WFFileStates { get; set; }
        DbSet<WFFileStateNote> WFFileStateNotes { get; set; }
        DbSet<FileType> FileTypes { get; set; }
        DbSet<Misc> Miscellaneous { get; set; }
        DbSet<WFWorkflow> WFWorkflows { get; set; }
        DbSet<WFState> WFStates { get; set; }
        DbSet<WFTransition> WFTransitions { get; set; }
        DbSet<WFWorkflowTransition> WFWorkflowTransitions { get; set; }
        DbSet<WFWorkflowState> WFWorkflowStates { get; set; }
        DbSet<UserProject> UserProjects { get; set; }
        DbSet<WFGroup> WFGroups { get; set; }
        DbSet<WFNotifyPresetGroup> WFNotifyPresetGroups { get; set; }
        DbSet<WFProcess> WFProcesses { get; set; }
        DbSet<WFRequirement> WFRequirements { get; set; }
        DbSet<WFSecurityPresetGroup> WFSecurityPresetGroups { get; set; }
        DbSet<WFTransitionProcess> WFTransitionProcesses { get; set; }
        DbSet<WFTransitionRequirement> WFTransitionRequirements { get; set; }
        DbSet<WFUser> WFUsers { get; set; }
        DbSet<WFRole> WFRoles { get; set; }
        DbSet<WFWorkflowRelatedEntity> WFWorkflowRelatedEntities { get; set; }
        DbSet<Log> Logs { get; set; }

        DbSet<T> Set<T>() where T : class;
        Database CurrentDatabase { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void SetState(object o, EntityState newState);
        void Dispose();
       

    }
}
