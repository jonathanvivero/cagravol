using SGE.Cagravol.Domain.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System.Data.Entity.ModelConfiguration;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Identity
{
    public class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            this.HasKey<string>(k => k.Id);

            this.Property(p => p.Id)
                .HasColumnName("Id")
                .HasMaxLength(128);

            this.Property(p => p.Name)
                .HasColumnName("Name")
                .HasMaxLength(1024);

            this.Property(p => p.Surname)
                .HasColumnName("Surname")
                .HasMaxLength(1024);

            this.Property(p => p.Email)
                .HasColumnName("Email")
                .HasMaxLength(1024);

            this.Property(p => p.PasswordHash)
                .HasColumnName("PasswordHash")
                .HasMaxLength(1024);

            this.Property(p => p.SecurityStamp)
                .HasColumnName("SecurityStamp")
                .HasMaxLength(1024);

            this.Property(p => p.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(1024);

            this.Property(p => p.UserName)
                .HasColumnName("UserName")
                .HasMaxLength(1024);
            
            this.Property(p => p.IsActive)
                .HasColumnName("IsActive");

            this.Property(p => p.CreatedOn)
                .HasColumnName("CreatedOn");

            this.Property(p => p.UpdatedOn)
                .HasColumnName("UpdatedOn");

            this.Property(p => p.LastAccess)
                .HasColumnName("LastAccess");


            this.ToTable("User", SchemaDefinitions.Dbo);
        }
    }
}
