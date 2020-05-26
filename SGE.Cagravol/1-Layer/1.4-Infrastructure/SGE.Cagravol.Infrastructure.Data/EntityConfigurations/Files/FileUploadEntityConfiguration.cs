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
    public class FileUploadEntityConfiguration : EntityTypeConfiguration<FileUpload>
    {
        public FileUploadEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.CustomerId)
            .HasColumnName("CustomerId");

            this.Property(i => i.FileTypeId)
            .HasColumnName("FileTypeId");
            
            this.Property(i => i.MimeType)
            .HasColumnName("MimeType")
            .HasMaxLength(1024);

            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(1024);

            this.Property(i => i.CustomerLogicalName)
            .HasColumnName("CustomerLogicalName")
            .HasMaxLength(1024);

            this.Property(i => i.ChannelId)
            .HasColumnName("ChannelId")
            .HasMaxLength(1024);
         
            this.Property(i => i.PartsCounter)
            .HasColumnName("PartsCounter");
            
            this.Property(i => i.PartsTotal)
            .HasColumnName("PartsTotal");

            this.Property(i => i.IsCompleted)
            .HasColumnName("IsCompleted");
            
            this.Property(i => i.Size)
            .HasColumnName("Size");

            this.Property(i => i.UploadPartsMapCode)
            .HasColumnName("UploadPartsMapCode")
            .HasColumnType("nvarchar(max)");

            this.Property(i => i.FileNotes)
            .HasColumnName("FileNotes")
            .HasColumnType("nvarchar(max)");


            this.Property(i => i.TempFolder)
            .HasColumnName("TempFolder")
            .HasColumnType("nvarchar(max)");

            this.Property(i => i.StartDate)
            .HasColumnName("StartDate");

            this.Property(i => i.LastHashUploadDate)
            .HasColumnName("LastHashUploadDate");

            this.ToTable("FileUpload", SchemaDefinitions.Dbo);

        }
    }
}
