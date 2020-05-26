using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Common
{
    public class AppConfigurationEntityConfiguration : EntityTypeConfiguration<AppConfiguration>
    {
        public AppConfigurationEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.Key)
            .HasColumnName("Key")
            .HasMaxLength(1024);

            this.Property(i => i.Value)
            .HasColumnName("Value")
            .HasColumnType("nvarchar(max)");

            this.Property(i => i.FieldType)
            .HasColumnName("FieldType");

            this.Property(i => i.IsPublicEnable)
            .HasColumnName("IsPublicEnable");

            this.Property(i => i.IsPublicViewable)
            .HasColumnName("IsPublicViewable");
            
            this.ToTable("AppConfiguration", SchemaDefinitions.Dbo);
        }

    }
}
