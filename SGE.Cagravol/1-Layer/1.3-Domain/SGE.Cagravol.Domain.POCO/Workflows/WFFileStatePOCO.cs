using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Domain.POCO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.Workflows
{
    public class WFFileStatePOCO
    {
        public long Id { get; set; }        
        
        public UserPOCO User { get; set; }
        public DateTime TS { get; set; }
        public string Description { get; set; }
        public WFStatePOCO State { get; set; }
        public IEnumerable<WFFileStateNotePOCO> Notes { get; set; }
        
        public WFFileStatePOCO()
        {
        }

        public WFFileStatePOCO(WFFileState wfState)
        {
            this.Id = wfState.Id;
            this.TS = wfState.TS;

            this.Notes = wfState.Notes.Select(s => new WFFileStateNotePOCO(s));
            this.State = new WFStatePOCO(wfState.WFState);
            this.User = new UserPOCO(wfState.User);

            
        }
    }
}
