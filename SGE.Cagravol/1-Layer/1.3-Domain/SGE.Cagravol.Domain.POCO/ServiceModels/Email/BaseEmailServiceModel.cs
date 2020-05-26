using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.ServiceModels.Email
{
    [Serializable]
    public class BaseEmailServiceModel
    {
        public string Subject { get; set; }
    }
}
