using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.ServiceModels.Email
{
    [Serializable]
    public class ResetPasswordEmailServiceModelModel
    : BaseEmailServiceModel
    {
        public string Body { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
    }
}
