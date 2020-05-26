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
    public class WFTransitionProcessEntityConfiguration: EntityTypeConfiguration<WFTransitionProcess>
    {
        public WFTransitionProcessEntityConfiguration()
        {
            this.Ignore(i => i.Id);

            this.HasKey(i => new { i.WFTransitionId, i.WFProcessId });

            this.Property(i => i.WFTransitionId)
            .HasColumnName("WFTransitionId");

            this.Property(i => i.WFProcessId)
            .HasColumnName("WFProcessId");
            
            this.ToTable("WFTransitionProcess", SchemaDefinitions.Dbo);

        }
    }
}
