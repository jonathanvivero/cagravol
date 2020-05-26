using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System.Data.Entity.ModelConfiguration;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Identity
{
    public class UserRoleEntityConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {
        public UserRoleEntityConfiguration()
        {
            this.HasKey(r => new { r.UserId, r.RoleId });

            this.Property(p => p.RoleId)
            .HasMaxLength(128);

            this.Property(p => p.UserId)
            .HasMaxLength(128);            

            this.ToTable("UserRole", SchemaDefinitions.Dbo);
        }
    }
}
