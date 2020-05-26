using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Files
{
    public class FileEntityConfiguration : EntityTypeConfiguration<File>
    {
        public FileEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.CustomerId)
            .HasColumnName("CustomerId");
            
            this.Property(i => i.FileTypeId)
            .HasColumnName("FileTypeId");

            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(1024);

            this.Property(i => i.URL)
            .HasColumnName("URL")
            .HasColumnType("nvarchar(max)");

            this.Property(i => i.ChannelId)
            .HasColumnName("ChannelId")
            .HasMaxLength(1024);

            this.Property(i => i.Version)
            .HasColumnName("Version");

            this.Property(i => i.MimeType)
            .HasColumnName("MimeType")
            .HasMaxLength(1024);

            this.Property(i => i.FileName)
            .HasColumnName("FileName")
            .HasMaxLength(1024);

            this.Property(i => i.Size)
            .HasColumnName("Size");

            this.Property(i => i.FileNotes)
            .HasColumnName("FileNotes")
            .HasColumnType("nvarchar(max)");
            
            this.Property(i => i.FirstDeliveryDate)
            .HasColumnName("FirstDeliveryDate");

            this.Property(i => i.WFWorkflowId)
            .HasColumnName("WFWorkflowId")
            .IsOptional();

            this.Property(i => i.WFWorkflowVersion)
            .HasColumnName("WFWorkflowVersion")
            .IsOptional();

            this.Ignore(i => i.WFCurrentEntityStateId);
            this.Ignore(i => i.WFCurrentStateId);

            this.HasMany<WFFileState>(p => p.History)
            .WithRequired(fk => fk.Entity)
            .HasForeignKey(fk => fk.EntityId)
            .WillCascadeOnDelete(true);

            this.ToTable("File", SchemaDefinitions.Dbo);

        }
    }
}
