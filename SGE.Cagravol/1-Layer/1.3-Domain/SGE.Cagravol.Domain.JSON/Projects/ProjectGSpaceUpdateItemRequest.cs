using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Projects
{
    public class ProjectGSpaceUpdateItemRequest
    {        
        public long id { get; set; }
        public string name { get; set; }
        public long fileTypeId { get; set; }
        public long projectId { get; set; }
        public string userName { get; set; }
    }
}
