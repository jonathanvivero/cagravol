using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Files
{
    public class FileUpload 
        : IEntityIdentity
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string MimeType { get; set; }
        public long FileTypeId { get; set; }
        public string Name { get; set; }
        public string CustomerLogicalName{get;set;}
        public string ChannelId { get; set; }
        public long PartsCounter { get; set; }
        public long PartsTotal { get; set; }
        public long Size {get;set;}
        public string TempFolder {get;set;}
        public bool IsCompleted { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastHashUploadDate { get; set; }
        public string UploadPartsMapCode {get;set;}
        public string FileNotes { get; set; }

        public virtual Customer Customer { get; set; }

    }
}
