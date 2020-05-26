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
    public class WFFileEntityNoteEntityConfiguration : EntityTypeConfiguration<WFFileStateNote>
    {
        public WFFileEntityNoteEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.UserId)
            .HasColumnName("UserId")
            .HasMaxLength(128);

            this.Property(i => i.WFEntityStateId)
            .HasColumnName("WFEntityStateId");

            this.Property(i => i.Comment)
            .HasColumnName("Comment")
            .HasColumnType("nvarchar(max)");

            this.Property(i => i.TS)
            .HasColumnName("TS");            
                       
            this.ToTable("WFFileStateNote", SchemaDefinitions.Dbo);

        }
    }
}
