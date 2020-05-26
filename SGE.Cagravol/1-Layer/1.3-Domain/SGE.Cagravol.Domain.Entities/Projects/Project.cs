using Newtonsoft.Json;
using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;

namespace SGE.Cagravol.Domain.Entities.Projects
{
    public class Project 
        : IEntityIdentity
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

        
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<UserProject> UserProjects { get; set; }

        public Project()
        {           
            this.Customers = new HashSet<Customer>();
            this.UserProjects = new HashSet<UserProject>();

        }


    }
}
