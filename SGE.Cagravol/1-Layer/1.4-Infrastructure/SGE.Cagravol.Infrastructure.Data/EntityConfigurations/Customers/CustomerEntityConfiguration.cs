using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Customers
{
    public class CustomerEntityConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.ProjectId)
            .HasColumnName("ProjectId");

            this.Property(i => i.BillDataTypeId)
            .HasColumnName("BillDataTypeId");

            this.Property(i => i.UserId)
            .HasColumnName("UserId")
            .HasMaxLength(128); 

            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(1024);

            this.Property(i => i.Notes)
           .HasColumnName("Notes")
           .HasColumnType("nvarchar(max)"); 

            this.Property(i => i.IsGeneric)
            .HasColumnName("IsGeneric");

            this.Property(i => i.Registered)
            .HasColumnName("Registered");

            this.Property(i => i.Reserved)
            .HasColumnName("Reserved");

            this.Property(i => i.Email)
            .HasColumnName("Email")
            .HasMaxLength(1024);

            this.Property(i => i.PasswordHash)
            .HasColumnName("PasswordHash")
            .HasMaxLength(2048);

            this.Property(i => i.SignUpCode)
            .HasColumnName("SignUpCode")
            .HasMaxLength(1024);

            this.Property(i => i.BillLegalIdentification)
            .HasColumnName("BillLegalIdentification")
            .HasColumnType("nvarchar(max)"); 

            this.Property(i => i.BillAddress)
            .HasColumnName("BillAddress")
            .HasColumnType("nvarchar(max)"); 

            this.Property(i => i.BillPostalCode)
            .HasColumnName("BillPostalCode")
            .HasMaxLength(64);

            this.Property(i => i.BillCity)
            .HasColumnName("BillCity")
            .HasMaxLength(1024);

            this.Property(i => i.BillCountry)
            .HasColumnName("BillCountry")
            .HasMaxLength(1024);

            this.Property(i => i.SpaceNumber)
            .HasColumnName("SpaceNumber");

            this.HasMany<File>(p => p.Files)
                .WithRequired(fk => fk.Customer)
                .HasForeignKey(fk => fk.CustomerId)
                .WillCascadeOnDelete(true);

            this.ToTable("Customer", SchemaDefinitions.Dbo);
        }
    }
}
