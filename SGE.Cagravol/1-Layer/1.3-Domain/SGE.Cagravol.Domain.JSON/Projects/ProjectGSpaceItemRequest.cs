using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Projects
{
    public class ProjectGSpaceItemRequest
    {        
        public long id { get; set; }
        public long projectId { get; set; }
        public string userName { get; set; }
    }
}
