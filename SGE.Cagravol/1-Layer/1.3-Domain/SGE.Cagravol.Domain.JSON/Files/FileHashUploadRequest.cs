using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Files
{
    public class FileHashUploadRequest
    {
        public string hash { get; set; }
        public long index { get; set; }
        public string channelId { get; set; }
        public long size { get; set; }
        public string userName { get; set; }

    }
}
