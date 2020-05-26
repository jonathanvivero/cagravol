using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Projects
{
    public class ProjectCustomerActivityRequest
    {
        public long id { get; set; }
        public long pId { get; set; }
        public string userName { get; set; }
    }
}
