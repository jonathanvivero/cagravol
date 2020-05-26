using SGE.Cagravol.Application.Core.Enums;
using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using SGE.Cagravol.Domain.Repositories.Workflows;
using SGE.Cagravol.Infrastructure.Data.Repositories.Base;
using SGE.Cagravol.Presentation.Resources.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Application.Core.Enums.Common;

namespace SGE.Cagravol.Infrastructure.Data.Repositories.Workflows
{
    public class WFStateRepository
        : BaseRepository<WFState>, IWFStateRepository
    {
        public WFStateRepository(ISGEContext context)
            : base(context)
        {
        }

        public IResultServiceModel<IEnumerable<WFState>> GetListByWorkflowId(long id)
        {
            return null;
        }

        public IResultServiceModel<IEnumerable<WFState>> GetListByTransitionOut(long id)
        {
            return null;
        }

        public IResultServiceModel<IEnumerable<WFState>> GetListByTransitionIn(long id)
        {
            return null;
        }

        public IResultServiceModel<WFState> GetInitialStateByWorkflow(long wfId)
        {
            IResultServiceModel<WFState> rsm = new ResultServiceModel<WFState>();

            try
            {

                var item = this.context
                    .WFWorkflowStates
                    .Where(w => w.IsInitial && w.WFWorkflowId == wfId)
                    .FirstOrDefault();

                if (item != null)
                {
                    rsm.OnSuccess(item.WFState);
                }
                else
                {
                    rsm.OnError(ErrorResources.ItemDoesNotExist, EnumErrorCode.ITEM_DOES_NOT_EXIST);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        public IResultServiceModel<WFState> GetInitialStateByWorkflowCode(string wfCode)
        {
            IResultServiceModel<WFState> rsm = new ResultServiceModel<WFState>();

            try
            {
                var item = this.context
                    .WFWorkflowStates
                    .Where(w => w.WFWorkflow.Code == wfCode && w.IsInitial)
                    .FirstOrDefault();

                if (item != null)
                {
                    rsm.OnSuccess(item.WFState);
                }
                else
                {
                    rsm.OnError(ErrorResources.ItemDoesNotExist, EnumErrorCode.ITEM_DOES_NOT_EXIST);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        public IResultServiceModel<WFState> GetSpecificStateByCode(string wfCode, string wfStateCode)
        {
            IResultServiceModel<WFState> rsm = new ResultServiceModel<WFState>();

            try
            {
                var item = this.context
                    .WFWorkflowStates
                    .Where(w => w.WFWorkflow.Code == wfCode
                        && w.WFState.Code == wfStateCode
                        && !w.IsInitial)
                    .FirstOrDefault();

                if (item != null)
                {
                    rsm.OnSuccess(item.WFState);
                }
                else
                {
                    rsm.OnError(ErrorResources.ItemDoesNotExist, EnumErrorCode.ITEM_DOES_NOT_EXIST);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        public IResultServiceModel<WFState> GetStateByWorkflowAndCode(long wfWorkflowId, long wfWorkflowVersion, string movementCode)
        {
            IResultServiceModel<WFState> rsm = new ResultServiceModel<WFState>();

            try
            {
                movementCode = movementCode.ToUpper();

                var wfState = this.context
                    .WFWorkflowStates
                    .Where(w => w.WFWorkflowId == wfWorkflowId
                        && w.WFWorkflowVersion == wfWorkflowVersion
                        && w.WFState.Code == movementCode)
                    .Select(s => s.WFState)
                    .FirstOrDefault();

                if (wfState != null)
                {
                    rsm.OnSuccess(wfState);
                }
                else
                {
                    rsm.OnError(ErrorResources.WFStateNotFound, EnumErrorCode.STATE_NOT_FOUND);
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
