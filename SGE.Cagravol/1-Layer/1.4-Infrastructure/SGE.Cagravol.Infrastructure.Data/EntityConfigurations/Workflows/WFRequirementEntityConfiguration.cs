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
    public class WFRequirementEntityConfiguration: EntityTypeConfiguration<WFRequirement>
    {
        public WFRequirementEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(1024);

            this.Property(i => i.Description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar(max)");

            this.Property(i => i.Code)
            .HasColumnName("Code")
            .HasMaxLength(1024);

            this.Property(i => i.EntityType)
            .HasColumnName("EntityType");

            this.ToTable("WFRequirement", SchemaDefinitions.Dbo);

        }
    }
}
