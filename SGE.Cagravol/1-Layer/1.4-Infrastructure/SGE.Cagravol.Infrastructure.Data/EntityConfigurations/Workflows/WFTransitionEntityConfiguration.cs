
using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Workflows
{
    public class WFTransitionEntityConfiguration : EntityTypeConfiguration<WFTransition>
    {
        public WFTransitionEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            //this.Property(i => i.WFWorkflowId)
            //.HasColumnName("WFWorkflowId");

            //this.Property(i => i.WFWorkflowVersion)
            //.HasColumnName("WFWorkflowVersion");

            this.Property(i => i.WFNotificationGroupId)
            .HasColumnName("WFNotificationGroupId")
            .IsOptional();

            this.Property(i => i.WFNotificationPresetGroupId)
            .HasColumnName("WFNotificationPresetGroupId")
            .IsOptional();

            this.Property(i => i.WFDefaultStateOriginId)
            .HasColumnName("WFDefaultStateOriginId");

            this.Property(i => i.WFDefaultStateDestinationId)
            .HasColumnName("WFDefaultStateDestinationId");

            this.Property(i => i.CouldComment)
            .HasColumnName("CouldComment");

            this.Property(i => i.MustComment)
            .HasColumnName("MustComment");

            this.Property(i => i.Code)
            .HasColumnName("Code");

            this.HasMany<WFTransitionProcess>(p => p.WFProcesses)
            .WithRequired(fk => fk.WFTransition)
            .HasForeignKey(fk => fk.WFTransitionId)
            .WillCascadeOnDelete(false);

            this.HasMany<WFTransitionRequirement>(p => p.WFRequirements)
            .WithRequired(fk => fk.WFTransition)
            .HasForeignKey(fk => fk.WFTransitionId)
            .WillCascadeOnDelete(false);

            this.HasMany<WFWorkflowTransition>(p => p.WFWorkflows)
            .WithRequired(fk => fk.WFTransition)
            .HasForeignKey(fk => fk.WFTransitionId)
            .WillCascadeOnDelete(false);

            this.ToTable("WFTransition", SchemaDefinitions.Dbo);

        }
    }
}
