using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;


namespace SGE.Cagravol.Domain.Entities.Identity
{
	public class User : IdentityUser //<string, UserLogin, UserRole, UserClaim>
	{        
        public bool IsActive { get; set; }		
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
		public DateTime LastAccess { get; set; }
        
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<WFEntityState<File>> FileHistoryItems { get; set; }
        public virtual ICollection<WFEntityStateNote<File>> FileHistoryItemNotes { get; set; }
        public virtual ICollection<UserProject> UserProjects { get; set; }        

        public User()
            : base()
        {
            this.Customers = new HashSet<Customer>();
            this.FileHistoryItems = new HashSet<WFEntityState<File>>();
            this.FileHistoryItemNotes = new HashSet<WFEntityStateNote<File>>();
            this.UserProjects = new HashSet<UserProject>();
        }

	
	}
}
