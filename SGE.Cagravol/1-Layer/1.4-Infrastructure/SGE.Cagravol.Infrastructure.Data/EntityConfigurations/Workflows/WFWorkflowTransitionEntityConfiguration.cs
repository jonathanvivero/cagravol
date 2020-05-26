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
    public class WFWorkflowTransitionEntityConfiguration: EntityTypeConfiguration<WFWorkflowTransition>
    {
        public WFWorkflowTransitionEntityConfiguration()
        {
            this.HasKey(i => i.Id);
            
            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.WFTransitionId)
            .HasColumnName("WFTransitionId")
            .IsRequired();

            this.Property(i => i.WFWorkflowId)
            .HasColumnName("WFWorkflowId")
            .IsRequired();

            this.Property(i => i.WFWorkflowVersion)
            .HasColumnName("WFWorkflowVersion")
            .IsRequired();

            this.Property(i => i.WFWorkflowStateDestinationId)
            .HasColumnName("WFWorkflowStateDestinationId");

            this.Property(i => i.WFWorkflowStateOriginId)
            .HasColumnName("WFWorkflowStateOriginId");

            this.ToTable("WFWorkflowTransition", SchemaDefinitions.Dbo);

        }
    }
}
