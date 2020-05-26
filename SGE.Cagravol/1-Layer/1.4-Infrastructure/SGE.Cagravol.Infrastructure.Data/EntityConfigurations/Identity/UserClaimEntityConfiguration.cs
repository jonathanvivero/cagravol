using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System.Data.Entity.ModelConfiguration;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Identity
{
    public class UserClaimEntityConfiguration : EntityTypeConfiguration<IdentityUserClaim>
    {
        public UserClaimEntityConfiguration()
        {
            this.HasKey<int>(l => l.Id);

            this.Property(p => p.UserId)
            .HasColumnName("UserId")
            .HasMaxLength(128);

            this.Property(p => p.ClaimValue)
            .HasColumnName("ClaimValue")
            .HasColumnType("nvarchar(max)");

            this.Property(p => p.ClaimType)
            .HasColumnName("ClaimType")
            .HasColumnType("nvarchar(max)"); 


            this.ToTable("UserClaim", SchemaDefinitions.Dbo);
        }
    }
}
