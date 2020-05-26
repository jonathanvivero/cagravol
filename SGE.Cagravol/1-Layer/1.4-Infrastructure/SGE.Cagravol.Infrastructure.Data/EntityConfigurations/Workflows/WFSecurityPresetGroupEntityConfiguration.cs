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
    public class WFSecurityPresetGroupEntityConfiguration: EntityTypeConfiguration<WFSecurityPresetGroup>
    {
        public WFSecurityPresetGroupEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(1024);

            this.Property(i => i.WFGrantedGroupId)
            .HasColumnName("WFGrantedGroupId")
            .IsOptional();
            
            this.Property(i => i.WFDeniedGroupId)
            .HasColumnName("WFDeniedGroupId")
            .IsOptional();                      

            this.ToTable("WFSecurityPresetGroup", SchemaDefinitions.Dbo);

        }
    }
}
