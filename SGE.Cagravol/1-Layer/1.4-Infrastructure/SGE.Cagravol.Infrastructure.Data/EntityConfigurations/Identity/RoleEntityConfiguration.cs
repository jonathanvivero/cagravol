using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System.Data.Entity.ModelConfiguration;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Identity
{
    public class RoleEntityConfiguration : EntityTypeConfiguration<IdentityRole>
    {
        public RoleEntityConfiguration()
        {
            this.HasKey<string>(k => k.Id);

            this.Property(p => p.Id)
            .HasColumnName("Id")
            //.HasColumnType("nvarchar(128)");
            .HasMaxLength(128);

            this.Property(p => p.Name)
            .HasColumnName("Name")
            //.HasColumnType("nvarchar(256)"); 
            .HasMaxLength(256);

            this.ToTable("Role", SchemaDefinitions.Dbo);
        }
    }
}
