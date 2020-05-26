using SGE.Cagravol.Application.Core.Enums.Common;
using SGE.Cagravol.Application.Core.Enums.Workflows;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Presentation.Resources.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public class WFEntityProcess<T>
        : IWFEntityProcess<T>
    {

        public WFEntityProcess()
        { }

        public WFEntityProcess(T Entity)
        {
            this.Entity = Entity;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public WFEntityTypeEnum EntityType { get; set; }
        public T Entity { get; set; }
        public IResultModel ExecuteProcess()
        {
            IResultModel rm = new ResultModel();
            if (this.Entity == null)
            {
                rm.OnSuccess();
            }
            else
            {
                rm.OnError(ErrorResources.ItemDoesNotExist, EnumErrorCode.ITEM_DOES_NOT_EXIST);
            }
            return rm;
        }

        public IResultModel ExecuteProcess(T entity)
        {
            IResultModel rm = new ResultModel();
            if (entity == null)
            {
                this.Entity = entity;
                rm.OnSuccess();
            }
            else
            {
                rm.OnError(ErrorResources.ItemDoesNotExist, EnumErrorCode.ITEM_DOES_NOT_EXIST);
            }
            return rm;

        }
    }
}
