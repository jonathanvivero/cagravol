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
    public class WFTransitionRequirementEntityConfiguration: EntityTypeConfiguration<WFTransitionRequirement>
    {
        public WFTransitionRequirementEntityConfiguration()
        {
            this.Ignore(i => i.Id);

            this.HasKey(i => new { i.WFTransitionId, i.WFRequirementId });

            this.Property(i => i.WFTransitionId)
            .HasColumnName("WFTransitionId");

            this.Property(i => i.WFRequirementId)
            .HasColumnName("WFRequirementId");

            this.ToTable("WFTransitionRequirement", SchemaDefinitions.Dbo);

        }
    }
}
