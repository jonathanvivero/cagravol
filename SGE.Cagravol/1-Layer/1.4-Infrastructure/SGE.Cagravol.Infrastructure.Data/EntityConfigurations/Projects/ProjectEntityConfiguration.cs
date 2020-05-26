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
    public class ProjectEntityConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectEntityConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
            .HasColumnName("Id");

            this.Property(i => i.Name)
            .HasColumnName("Name")
            .HasMaxLength(1024);

            this.Property(i => i.Description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar(max)"); 

            this.Property(i => i.StartDate)
            .HasColumnName("StartDate");

            this.Property(i => i.FinishDate)
            .HasColumnName("FinishDate");

            this.Property(i => i.ExtraChargeForSendingDate)
            .HasColumnName("ExtraChargeForSendingDate");

            this.Property(i => i.LimitForSendingDate)
            .HasColumnName("LimitForSendingDate");

            this.Property(i => i.ExtraChargePercentage)
            .HasColumnName("ExtraChargePercentage");

            this.Property(i => i.Notes)
            .HasColumnName("Notes")
            .HasColumnType("nvarchar(max)"); 

            this.Property(i => i.TotalStands)
            .HasColumnName("TotalStands");

            this.Property(i => i.Code)
            .HasColumnName("Code");

            this.HasMany<Customer>(p => p.Customers)
              .WithRequired(fk => fk.Project)
              .HasForeignKey(fk => fk.ProjectId)
              .WillCascadeOnDelete(true);

            this.HasMany<UserProject>(p => p.UserProjects)
              .WithRequired(fk => fk.Project)
              .HasForeignKey(fk => fk.ProjectId)
              .WillCascadeOnDelete(true);

            this.ToTable("Project", SchemaDefinitions.Dbo);

        }
    }
}
