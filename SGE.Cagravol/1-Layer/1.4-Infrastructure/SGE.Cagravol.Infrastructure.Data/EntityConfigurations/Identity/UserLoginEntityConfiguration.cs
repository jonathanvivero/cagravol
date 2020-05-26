using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System.Data.Entity.ModelConfiguration;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Identity
{
    public class UserLoginEntityConfiguration : EntityTypeConfiguration<IdentityUserLogin>
    {
        public UserLoginEntityConfiguration()
        {
            this.HasKey(l => new { l.UserId, l.ProviderKey, l.LoginProvider });

            this.Property(p => p.UserId)
            .HasColumnName("UserId")
            .HasMaxLength(128);

            this.Property(p => p.ProviderKey)
            .HasColumnName("ProviderKey")
            .HasMaxLength(128);

            this.Property(p => p.LoginProvider)
            .HasColumnName("LoginProvider")
            .HasMaxLength(128);

            this.ToTable("UserLogin", SchemaDefinitions.Dbo);
        }
    }
}
