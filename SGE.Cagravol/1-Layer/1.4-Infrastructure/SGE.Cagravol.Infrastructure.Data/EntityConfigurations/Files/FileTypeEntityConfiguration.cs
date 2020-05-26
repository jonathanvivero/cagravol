using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Files
{
    public class FileTypeEntityConfiguration : EntityTypeConfiguration<FileType>
    {
        public FileTypeEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(128);

            this.Property(i => i.Notes)
            .HasColumnName("Notes")
            .HasColumnType("nvarchar(max)"); 

            this.Property(i => i.FileExtension)
            .HasColumnName("FileExtension")
            .HasMaxLength(256);

            this.HasMany<File>(p => p.Files)
                .WithRequired(fk => fk.FileType)
                .HasForeignKey(fk => fk.FileTypeId)
                .WillCascadeOnDelete(false);

            this.ToTable("FileType", SchemaDefinitions.Dbo);

        }
    }
}
