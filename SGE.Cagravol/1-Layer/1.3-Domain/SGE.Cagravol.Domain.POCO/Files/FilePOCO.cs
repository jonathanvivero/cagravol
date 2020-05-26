using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.POCO.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.Files
{
    public class FilePOCO        
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long FileTypeId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string ChannelId { get; set; }
        public int Version { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public string FileNotes { get; set; }
        public long Size { get; set; }
        public DateTime FirstDeliveryDate { get; set; }
        public long? WFWorkflowId { get; set; }
        public long? WFWorkflowVersion { get; set; }

        public FileTypePOCO FileType { get; set; }
        public WFStatePOCO WFCurrentState { get; set; }
        
        public FilePOCO()
        {            
        }

        public FilePOCO(File file)
        {
            this.Id = file.Id;
            this.CustomerId = file.CustomerId;
            this.FileTypeId = file.FileTypeId;
            this.Name = file.Name;
            this.URL = file.URL;
            this.ChannelId = file.ChannelId;
            this.Version = file.Version;
            this.MimeType = file.MimeType;
            this.FileName = file.FileName;
            this.FileNotes = file.FileNotes;            
            this.Size = file.Size;
            this.FirstDeliveryDate = file.FirstDeliveryDate;
            this.WFWorkflowId = file.WFWorkflowId;
            this.WFWorkflowVersion = file.WFWorkflowVersion;
            if (file.FileType != null)
            { 
                this.FileType = new FileTypePOCO(file.FileType);
            }

            if (file.WFCurrentState != null)
            { 
                this.WFCurrentState = new WFStatePOCO(file.WFCurrentState);
            }

        }
    }
}
