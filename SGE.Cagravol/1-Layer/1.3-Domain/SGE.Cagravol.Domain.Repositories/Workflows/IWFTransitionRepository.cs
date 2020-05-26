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
    public interface IWFTransitionRepository
        : IBaseRepository<WFTransition>
    {
        IResultServiceModel<IEnumerable<WFTransition>> GetListByWorkflowId(long id);
        IResultServiceModel<IEnumerable<WFTransition>> GetListToState(long id);
        IResultServiceModel<IEnumerable<WFTransition>> GetListFromState(long id);
        IResultServiceModel<WFTransition> FindByWorkflowAndEndPointStates(long originWFStateId, long destinationWFStateId, long wfWorkflowId, long wfWorkflowVersion);
    }
}
