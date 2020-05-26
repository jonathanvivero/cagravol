using SGE.Cagravol.Domain.POCO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Files
{
    public class FileStateCommentRequest
    {
        public long id { get; set; }
        public long stateId { get; set; }
        public long fileId { get; set; }
        public string userName { get; set; }
        public string comment { get; set; }
        public DateTime ts { get; set; }
        public UserPOCO user { get; set; }
    }
}
