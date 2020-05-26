using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Infrastructure.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.EntityConfigurations.Projects
{
    public class UserProjectEntityConfiguration : EntityTypeConfiguration<UserProject>
    {
        public UserProjectEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.ProjectId)
            .HasColumnName("ProjectId");

            this.Property(i => i.UserId)
            .HasColumnName("UserId")
            .HasMaxLength(128);

            this.Property(i => i.IsAuthorized)
            .HasColumnName("IsAuthorized");

            this.Property(i => i.IsOwner)
            .HasColumnName("IsOwner");

            this.ToTable("UserProject", SchemaDefinitions.Dbo);

        }
    }
}
