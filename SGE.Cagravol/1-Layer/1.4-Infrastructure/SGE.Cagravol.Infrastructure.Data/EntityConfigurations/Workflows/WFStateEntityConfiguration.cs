using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Workflows
{
    public class WFStateEntityConfiguration : EntityTypeConfiguration<WFState>
    {
        public WFStateEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            //this.Property(i => i.WFWorkflowId)
            //.HasColumnName("WFWorkflowId");

            //this.Property(i => i.WFWorkflowVersion)
            //.HasColumnName("WFWorkflowVersion");

            this.Property(i => i.WFSecurityPresetGroupId)
            .HasColumnName("WFSecurityPresetGroupId")
            .IsOptional();

            this.Property(i => i.WFGrantedGroupId)
            .HasColumnName("WFGrantedGroupId")
            .IsOptional();

            this.Property(i => i.WFDeniedGroupId)
            .HasColumnName("WFDeniedGroupId")
            .IsOptional();
            
            this.Property(i => i.Code)
            .HasColumnName("Code")
            .HasMaxLength(1024);

            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(1024);

            this.Property(i => i.Description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar(max)");

            this.HasMany<WFTransition>(p => p.WFDefaultTransitionsFrom)
            .WithRequired(fk => fk.WFDefaultStateDestination)
            .HasForeignKey(fk => fk.WFDefaultStateDestinationId)
            .WillCascadeOnDelete(false);

            this.HasMany<WFTransition>(p => p.WFDefaultTransitionsTo)
            .WithRequired(fk => fk.WFDefaultStateOrigin)
            .HasForeignKey(fk => fk.WFDefaultStateOriginId)
            .WillCascadeOnDelete(false);

            this.HasMany<WFWorkflowState>(p => p.WFWorkflows)
            .WithRequired(fk => fk.WFState)
            .HasForeignKey(fk => fk.WFStateId)
            .WillCascadeOnDelete(false);

            this.ToTable("WFState", SchemaDefinitions.Dbo);

        }
    }
}
