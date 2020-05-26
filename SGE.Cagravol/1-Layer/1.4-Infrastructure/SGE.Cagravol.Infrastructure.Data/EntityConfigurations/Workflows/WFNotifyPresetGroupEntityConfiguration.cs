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
    public class WFNotifyPresetGroupEntityConfiguration: EntityTypeConfiguration<WFNotifyPresetGroup>
    {
        public WFNotifyPresetGroupEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(1024); 

            this.Property(i => i.WFNotificationGroupId)
            .HasColumnName("WFNotificationGroupId")
            .IsOptional();                      

            this.ToTable("WFNotifyPresetGroup", SchemaDefinitions.Dbo);

        }
    }
}
