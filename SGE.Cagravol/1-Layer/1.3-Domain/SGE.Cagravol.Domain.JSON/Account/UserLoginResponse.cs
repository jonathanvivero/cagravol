using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Account
{
    public class UserLoginResponse
    {
        public IResultModel result { get; set; }
        public long CustomerId { get; set; }
        public string TokenId { get; set; }
        public string ProjectId { get; set; }

    }
}
