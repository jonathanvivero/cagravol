using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Application.ServiceModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Application.Core.Enums.Workflows;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;

namespace SGE.Workflow
{
    public interface IWFEngine<T>
        where T: IEntityIdentity
    {
        
        IResultModel ExecuteTransition(T entity, IWFEntityTransition<T> transition);
        WFEntityTypeEnum GetEntityType();
    }
}
