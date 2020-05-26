using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Workflows
{
    public interface IWFStateRepository
        : IBaseRepository<WFState>
    {
        IResultServiceModel<IEnumerable<WFState>> GetListByWorkflowId(long id);        
        IResultServiceModel<IEnumerable<WFState>> GetListByTransitionOut(long id);
        IResultServiceModel<IEnumerable<WFState>> GetListByTransitionIn(long id);

        IResultServiceModel<WFState> GetInitialStateByWorkflow(long wfId);
        IResultServiceModel<WFState> GetInitialStateByWorkflowCode(string wfCode);
        IResultServiceModel<WFState> GetSpecificStateByCode(string wfCode, string wfStateCode);
        IResultServiceModel<WFState> GetStateByWorkflowAndCode(long wfWorkflowId, long wfWorkflowVersion, string movementCode);
    }
}
