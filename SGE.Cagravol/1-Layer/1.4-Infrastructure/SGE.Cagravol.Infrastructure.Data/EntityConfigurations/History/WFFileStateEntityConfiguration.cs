using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.History
{
    public class WFFileStateEntityConfiguration : EntityTypeConfiguration<WFFileState>
    {
        public WFFileStateEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.EntityId)
            .HasColumnName("EntityId");

            this.Property(i => i.WFStateId)
            .HasColumnName("WFStateId");

            this.Property(i => i.UserId)
            .HasColumnName("UserId")
            .HasMaxLength(128);

            this.Property(i => i.TS)
            .HasColumnName("TS");

            this.HasMany<WFFileStateNote>(p => p.Notes)
              .WithRequired(fk => fk.WFEntityState)
              .HasForeignKey(fk => fk.WFEntityStateId)
              .WillCascadeOnDelete(true);
  
            this.ToTable("WFFileState", SchemaDefinitions.Dbo);

        }
    }
}
