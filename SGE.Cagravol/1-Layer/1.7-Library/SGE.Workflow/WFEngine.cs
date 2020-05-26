using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Application.Core.Enums.Workflows;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;

namespace SGE.Workflow
{
    public class WFEngine<T>
        : IWFEngine<T>
        where T : IEntityIdentity
    {
        protected readonly WFEntityTypeEnum currentEntityType;
        protected readonly ISGEContext context;
        public WFEngine(ISGEContext context)
        {
            this.context = context;
            this.currentEntityType = this.GetEntityType();
        }

        public IResultModel ExecuteTransition(T entity, IWFEntityTransition<T> transition)
        {
            IResultModel rm = new ResultModel();

            return rm;
        }

        #region Private Methods

        public WFEntityTypeEnum GetEntityType()
        {
            WFEntityTypeEnum result;
            Type typeParameterType = typeof(T);

            if (typeParameterType == typeof(File))
            {
                result = WFEntityTypeEnum.File;
            }
            else
            {
                result = WFEntityTypeEnum.Generic;
            }

            return result;
        }

        #endregion
    }

}
