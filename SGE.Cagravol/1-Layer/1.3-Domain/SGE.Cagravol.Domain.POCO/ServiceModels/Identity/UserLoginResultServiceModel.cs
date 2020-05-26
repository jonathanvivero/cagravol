using SGE.Cagravol.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.ServiceModels.Identity
{
    public class UserLoginResultServiceModel
    {
        public bool Succeeded { get; set; }
		public string FailMessage { get; set; }
		public User LoggedUser { get; set; } 
		public UserLoginResultServiceModel() { }
		public UserLoginResultServiceModel(bool succeeded, string failMessage)
		{
			this.FailMessage = failMessage;
			this.Succeeded = succeeded;
		}

        public UserLoginResultServiceModel(bool succeeded, string failMessage, User user)
		{
			this.FailMessage = failMessage;
			this.Succeeded = succeeded;
			this.LoggedUser = user;
		}
    }
}
