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
    public class LogEntityConfiguration : EntityTypeConfiguration<Log>
    {
        public LogEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.Notes)
            .HasColumnName("Notes")
            .HasColumnType("nvarchar(max)");
            
            this.ToTable("Log", SchemaDefinitions.Dbo);
        }

    }
}
