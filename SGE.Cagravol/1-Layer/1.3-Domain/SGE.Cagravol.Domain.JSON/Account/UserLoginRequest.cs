using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Account
{
    public class UserLoginRequest
    {
        public string CustomerEmail { get; set; }        
        public string CustomerPasswordHash { get; set; }
        public int WrongTries { get; set; }
    }
}
