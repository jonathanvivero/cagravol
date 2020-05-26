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
    public class WorkflowRepository
        : BaseRepository<WFWorkflow>, IWorkflowRepository
    {
        public WorkflowRepository(ISGEContext context)
            : base(context)
        {

        }

        public IResultServiceModel<long> GetWorkflowIdByCode(string code)
        {
            IResultServiceModel<long> rsm = new ResultServiceModel<long>();

            try
            {
                long? id = this.context
                    .WFWorkflows
                    .Where(w => w.Code == code)
                    .Select(s => s.Id)
                    .SingleOrDefault();

                if (id.HasValue)
                {
                    rsm.OnSuccess(id.Value);
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

        public IResultServiceModel<WFWorkflow> GetWorkflowByCode(string code)
        {
            IResultServiceModel<WFWorkflow> rsm = new ResultServiceModel<WFWorkflow>();

            try
            {
                var item = this.context
                    .WFWorkflows
                    .Where(w => w.Code == code)                    
                    .FirstOrDefault();

                if (item != null)
                {
                    rsm.OnSuccess(item);
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


        public IResultServiceModel<WFWorkflow> GetById(long id)
        {
            IResultServiceModel<WFWorkflow> rsm = new ResultServiceModel<WFWorkflow>();

            try
            {
                var item = this.context
                    .WFWorkflows
                    .Where(w => w.Id == id)
                    .FirstOrDefault();

                if (item != null)
                {
                    rsm.OnSuccess(item);
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
    }
}
