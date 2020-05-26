using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Customers
{
    public class BillDataTypeEntityConfiguration : EntityTypeConfiguration<BillDataType>
    {

        public BillDataTypeEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(1024);

            this.Property(i => i.I18NCode)
            .HasColumnName("I18NCode")
            .HasMaxLength(1024) ;

            this.Property(i => i.Notes)
            .HasColumnName("Notes")
            .HasColumnType("nvarchar(max)");


            this.ToTable("BillDataType", SchemaDefinitions.Dbo);
        }
    }
}
