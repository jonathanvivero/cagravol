using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Domain.POCO.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Projects
{
    public class ProjectListResponse
    {        
        public IEnumerable<ProjectPOCO> list {get;set;}

    }
}
