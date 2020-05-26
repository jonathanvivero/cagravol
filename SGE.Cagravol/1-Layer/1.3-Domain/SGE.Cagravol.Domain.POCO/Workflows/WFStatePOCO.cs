using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.Workflows
{
    public class WFStatePOCO
    {
        public long Id { get; set; }
        public long? WFSecurityPresetGroupId { get; set; }
        public long? WFGrantedGroupId { get; set; }
        public long? WFDeniedGroupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }                              
        
        public WFStatePOCO()
        {
        }

        public WFStatePOCO(WFState wfState)
        {
            this.Id = wfState.Id;
            this.WFSecurityPresetGroupId = wfState.WFSecurityPresetGroupId;
            this.WFGrantedGroupId = wfState.WFGrantedGroupId;
            this.WFDeniedGroupId = wfState.WFDeniedGroupId;
            this.Code = wfState.Code;
            this.Name = wfState.Name;
            this.Description = wfState.Description;            
        }
    }
}
