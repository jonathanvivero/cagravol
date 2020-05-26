using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Files
{
    public class FileEndUploadRequest
    {               
        public string channelId { get; set; }
        public long fileId { get; set; }
        public long projectId { get; set; }
        public string userName { get; set; }

    }
}
