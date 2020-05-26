using SGE.Cagravol.Domain.POCO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Files
{
    public class FileStateCommentResponse
    {
        public long temporalId { get; set; }
        public long newId { get; set; }
        public long fileId { get; set; }
        public long stateId { get; set; }                
    }
}
