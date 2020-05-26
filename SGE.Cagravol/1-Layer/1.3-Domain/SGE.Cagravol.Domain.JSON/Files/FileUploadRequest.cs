using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Files
{
    public class FileUploadRequest
    {
        public string channelId { get; set; }
        public long size { get; set; }
        public string mimeType { get; set; }
        public string fileName { get; set; }
        public long fileTypeId { get; set; }
        public string fileNotes { get; set; }
        public long parts { get; set; }
        public string customerLogicalName { get; set; }
        public long projectId { get; set; }
        public string userName { get; set; }
    }
}
