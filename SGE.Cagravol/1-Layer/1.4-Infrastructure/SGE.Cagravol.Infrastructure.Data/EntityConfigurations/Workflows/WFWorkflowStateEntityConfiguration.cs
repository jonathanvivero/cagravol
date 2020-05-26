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
    public class WFWorkflowStateEntityConfiguration: EntityTypeConfiguration<WFWorkflowState>
    {
        public WFWorkflowStateEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.WFStateId)
            .HasColumnName("WFStateId")
            .IsRequired();

            this.Property(i => i.WFWorkflowId)
            .HasColumnName("WFWorkflowId")
            .IsRequired();
            
            this.Property(i => i.WFWorkflowVersion)
            .HasColumnName("WFWorkflowVersion")
            .IsRequired();

            this.Property(i => i.IsInitial)
            .HasColumnName("IsInitial");

            this.HasMany<WFWorkflowTransition>(p => p.WFWorkflowTransitionsFrom)
            .WithRequired(fk => fk.WFWorkflowStateDestination)
            .HasForeignKey(fk => fk.WFWorkflowStateDestinationId)
            .WillCascadeOnDelete(false);

            this.HasMany<WFWorkflowTransition>(p => p.WFWorkflowTransitionsTo)
            .WithRequired(fk => fk.WFWorkflowStateOrigin)
            .HasForeignKey(fk => fk.WFWorkflowStateOriginId)
            .WillCascadeOnDelete(false);


            this.ToTable("WFWorkflowState", SchemaDefinitions.Dbo);

        }
    }
}
