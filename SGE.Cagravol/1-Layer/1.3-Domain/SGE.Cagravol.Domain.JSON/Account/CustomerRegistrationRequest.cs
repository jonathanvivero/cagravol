using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Account
{
    public class CustomerRegistrationRequest
    {        
        public string projectCode { get; set; }
        public string userName { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }        
    }
}
