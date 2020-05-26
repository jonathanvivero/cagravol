using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.ServiceModels.Identity
{
    public class UserCreationResultServiceModel
    {
        public bool Succeeded { get; set; }
		public string FailMessage { get; set; }
		public User UserCreated {get;set;}
		
		public UserCreationResultServiceModel() { }
		public UserCreationResultServiceModel(User userCreated)
		{
			this.UserCreated = userCreated;
			this.Succeeded = true;
		}
        public UserCreationResultServiceModel(string failMessage)
		{
			this.FailMessage = failMessage;
			this.Succeeded = false;
		}
    }
}
