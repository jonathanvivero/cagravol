using SGE.Cagravol.Domain.Entities.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Files
{
    public class ProjectFilesListResponse
    {
        public string message { get; set; }
        public IEnumerable<File> fileList {get;set;}

    }
}
