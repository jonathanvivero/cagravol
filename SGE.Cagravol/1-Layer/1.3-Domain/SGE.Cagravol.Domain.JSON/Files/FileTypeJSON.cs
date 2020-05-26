using SGE.Cagravol.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.JSON.Files
{
    public class FileTypeJSON
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string FileExtension { get; set; }        
        
    }
}
