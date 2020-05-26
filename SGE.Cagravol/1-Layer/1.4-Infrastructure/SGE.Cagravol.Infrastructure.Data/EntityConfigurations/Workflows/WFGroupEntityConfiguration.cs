using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
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
    public class WFGroupEntityConfiguration: EntityTypeConfiguration<WFGroup>
    {
        public WFGroupEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(1024); 

            this.Property(i => i.IsPreset)
            .HasColumnName("IsPreset");

            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(1024);
            
            this.HasMany<WFRole>(p => p.Roles)
            .WithRequired(fk => fk.WFGroup)
            .HasForeignKey(fk => fk.WFGroupId)
            .WillCascadeOnDelete(true);

            this.HasMany<WFUser>(p => p.Users)
           .WithRequired(fk => fk.WFGroup)
           .HasForeignKey(fk => fk.WFGroupId)
           .WillCascadeOnDelete(true);

            this.ToTable("WFGroup", SchemaDefinitions.Dbo);

        }
    }
}
