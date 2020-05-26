using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.History;

namespace SGE.Cagravol.Domain.Repositories.Workflows
{
    public interface IWorkflowRepository
        : IBaseRepository<WFWorkflow>
    {

        IResultServiceModel<long> GetWorkflowIdByCode(string code);
        IResultServiceModel<WFWorkflow> GetWorkflowByCode(string code);
        IResultServiceModel<WFWorkflow> GetById(long id);
    }
}
