using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Domain.POCO.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.Projects
{
    public class ProjectPOCO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime ExtraChargeForSendingDate { get; set; }
        public DateTime LimitForSendingDate { get; set; }
        public double ExtraChargePercentage { get; set; }
        public string Notes { get; set; }
        public long TotalStands { get; set; }
        public string Code { get; set; }

        public IEnumerable<CustomerPOCO> Customers { get; set; }
        public IEnumerable<UserProjectPOCO> UserProjects { get; set; }

        public ProjectPOCO () {}
        public ProjectPOCO (Project entity){

            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.StartDate = entity.StartDate;
            this.FinishDate = entity.FinishDate;
            this.ExtraChargeForSendingDate = entity.ExtraChargeForSendingDate;
            this.LimitForSendingDate = entity.LimitForSendingDate;
            this.ExtraChargePercentage = entity.ExtraChargePercentage;
            this.Notes = entity.Notes;
            this.TotalStands = entity.TotalStands;
            this.Code = entity.Code;

            this.Customers = entity.Customers.OrderBy(o=>o.SpaceNumber).Select(s => new CustomerPOCO(s));
            this.UserProjects = entity.UserProjects.Select(s => new UserProjectPOCO(s));

        }

    }
}
