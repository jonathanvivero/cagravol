using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Files
{
    public class File 
        : IEntityIdentity, IWFEntityWorkflow<File>
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
        public long Size { get; set; }
        public DateTime FirstDeliveryDate { get; set; }
        public long? WFWorkflowId { get; set; }
        public long? WFWorkflowVersion { get; set; }
        public string FileNotes { get; set; }

        public virtual FileType FileType { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual WFWorkflow WFWorkflow { get; set; }
        public virtual ICollection<WFFileState> History { get; set; } 
                    //{ get { return (ICollection<IWFEntityState<File>>)this.FileHistory; } set { this.FileHistory = (ICollection<WFFileState>)value; } }
        //public virtual ICollection<WFEntityState<File>> FileHistory { get; set; }
        
        public File()
        {
            this.History = new HashSet<WFFileState>();
        }

        //Ignorable
        public long? WFCurrentStateId
        {
            get
            {
                WFEntityState<File> item = null;
                item = this.History.OrderByDescending(o => o.Id).FirstOrDefault();
                return (item == null) ? null : (long?)item.WFStateId;
            }
        }
        //Ignorable
        public long? WFCurrentEntityStateId
        {
            get
            {
                WFEntityState<File> item = null;
                item = this.History.OrderByDescending(o => o.Id).FirstOrDefault();
                return (item == null) ? null : (long?)item.Id;
            }
        }
        //Ignorable
        public WFState WFCurrentState
        {
            get
            {
                WFEntityState<File> item = null;
                item = this.History.OrderByDescending(o => o.Id).FirstOrDefault();
                return (item == null) ? null : item.WFState;
            }
        }
        //Ignorable
        public WFEntityState<File> WFCurrentEntityState
        {
            get
            {
                WFEntityState<File> item = null;
                item = this.History.OrderByDescending(o => o.Id).FirstOrDefault();
                return item;
            }
        }

    }
}
