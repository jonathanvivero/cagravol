using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Alfred
{
    public class SendTestEmailRequest
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string message { get; set; }
    }
}
