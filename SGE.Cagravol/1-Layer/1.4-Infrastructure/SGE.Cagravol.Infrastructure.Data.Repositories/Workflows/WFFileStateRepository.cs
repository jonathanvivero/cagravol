using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Domain.Repositories.Base;
using SGE.Cagravol.Domain.Repositories.History;
using SGE.Cagravol.Infrastructure.Data;
using SGE.Cagravol.Infrastructure.Data.Repositories.Base;
using SGE.Cagravol.Presentation.Resources.Common;
using SGE.Cagravol.Presentation.Resources.Workflows;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Workflows
{
    public class WFFileStateRepository
        : BaseRepository<WFFileState>, IWFFileStateRepository
    {
        public WFFileStateRepository(ISGEContext context)
            : base(context)
        {
        }

        public IResultServiceModel<IEnumerable<WFFileState>> GetListByFileId(long id)
        {
            IResultServiceModel<IEnumerable<WFFileState>> rsm = new ResultServiceModel<IEnumerable<WFFileState>>();
            try
            {
                var list = this.context
                    .WFFileStates
                    .Where(w => w.EntityId == id)
                    .OrderByDescending(o => o.TS)
                    .ToList();

                if (list != null)
                {
                    rsm.OnSuccess(list);
                }
                else
                {
                    rsm.OnError(HistoryResources.NoHistoryFound);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        public IResultServiceModel<WFFileState> GetById(long id)
        {
            IResultServiceModel<WFFileState> rsm = new ResultServiceModel<WFFileState>();

            var item = this.context
                .WFFileStates
                .Include(i => i.WFState)
                .Include(i => i.User)
                .Where(w => w.Id == id)
                .FirstOrDefault();

            if (item != null)
            {
                if (item.WFState == null)
                {
                    item.WFState = this.context.WFStates.Find(item.WFStateId);
                }
                rsm.OnSuccess(item);
            }
            else
            {
                rsm.OnError(ErrorResources.ItemDoesNotExist);
            }

            return rsm;
        }



        //public IResultServiceModel<WFFileState> GetFileStateByCode(long wfWorkflowId, long wfWorkflowVersion, string movementCode)
        //{
        //    IResultServiceModel<WFFileState> rsm = new ResultServiceModel<WFFileState>();

        //    try
        //    {
        //        movementCode = movementCode.ToUpper();

        //        var wfStateList = this.context
        //            .WFWorkflowStates
        //            .Where(w => w.WFWorkflowId == wfWorkflowId 
        //                && w.WFWorkflowVersion == wfWorkflowVersion 
        //                && w.WFStateId == wfStateId 
        //                && w.WFState.Code == movementCode);


        //        if (wfStateList.Any())
        //        {
        //            rsm.OnSuccess();
        //        }
        //        else
        //        {
        //            rsm.OnError();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        rsm.OnException(ex);

        //    }
        //    return rsm;
        //}
    }
}
