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
    public class WFWorkflowEntityConfiguration : EntityTypeConfiguration<WFWorkflow>
    {
        public WFWorkflowEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.WFWorkflowVersion)
            .HasColumnName("WFWorkflowVersion");
            
            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(1024);

            this.Property(i => i.Code)
            .HasColumnName("Code")
            .HasMaxLength(1024); 

            this.Property(i => i.Notes)
            .HasColumnName("Notes")
            .HasColumnType("nvarchar(max)"); 

            this.HasMany<WFWorkflowTransition>(p => p.WFTransitions)
            .WithRequired(fk => fk.WFWorkflow)
            .HasForeignKey(fk => fk.WFWorkflowId)
            .WillCascadeOnDelete(true);

            this.HasMany<WFWorkflowState>(p => p.WFStates)
            .WithRequired(fk => fk.WFWorkflow)
            .HasForeignKey(fk => fk.WFWorkflowId)
            .WillCascadeOnDelete(true);

            this.HasMany<WFWorkflowRelatedEntity>(p => p.WFWorkflowRelatedEntities)
            .WithRequired(fk => fk.WFWorkflow)
            .HasForeignKey(fk => fk.WFWorkflowId)
            .WillCascadeOnDelete(true);

            this.ToTable("WFWorkflow", SchemaDefinitions.Dbo);

        }
    }
}
