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
    public class WFRoleEntityConfiguration: EntityTypeConfiguration<WFRole>
    {
        public WFRoleEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.RoleId)
            .HasColumnName("RoleId")
            .HasMaxLength(128);

            this.Property(i => i.WFGroupId)
            .HasColumnName("WFGroupId");           

            this.ToTable("WFRole", SchemaDefinitions.Dbo);

        }
    }
}
