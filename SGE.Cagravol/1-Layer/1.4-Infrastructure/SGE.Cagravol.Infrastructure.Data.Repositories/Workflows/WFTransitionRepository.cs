using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using SGE.Cagravol.Domain.Repositories.Workflows;
using SGE.Cagravol.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Presentation.Resources.Common;
using SGE.Cagravol.Application.Core.Enums.Common;

namespace SGE.Cagravol.Infrastructure.Data.Repositories.Workflows
{
    public class WFTransitionRepository
        : BaseRepository<WFTransition>, IWFTransitionRepository
    {
        public WFTransitionRepository(ISGEContext context)
			: base(context)
		{
		}		

        public IResultServiceModel<IEnumerable<WFTransition>> GetListByWorkflowId(long id)
        {
            return null;
        }

        public IResultServiceModel<IEnumerable<WFTransition>> GetListToState(long id)
        {
            return null;
        }

        public IResultServiceModel<IEnumerable<WFTransition>> GetListFromState(long id)
        {
            return null;
        }

        public IResultServiceModel<WFTransition> FindByWorkflowAndEndPointStates(long originWFStateId, long destinationWFStateId, long wfWorkflowId, long wfWorkflowVersion)
        {

            IResultServiceModel<WFTransition> rsm = new ResultServiceModel<WFTransition>();

            try
            {
                var tr = this.context
                    .WFWorkflowTransitions
                    .Where(w => w.WFWorkflowId == wfWorkflowId
                        && w.WFWorkflowVersion == wfWorkflowVersion
                        && w.WFWorkflowStateOrigin.WFStateId == originWFStateId
                        && w.WFWorkflowStateDestination.WFStateId == destinationWFStateId)
                    .FirstOrDefault();

                if (tr != null)
                {
                    rsm.OnSuccess(tr.WFTransition);
                }
                else 
                {
                    rsm.OnError(ErrorResources.ItemDoesNotExist, EnumErrorCode.TRANSITION_NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);                
            }

            return rsm;
        }
    }
}
