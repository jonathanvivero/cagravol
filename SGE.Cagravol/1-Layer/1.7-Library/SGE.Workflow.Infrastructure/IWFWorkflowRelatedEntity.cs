using SGE.Cagravol.Application.Core.Enums.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFWorkflowRelatedEntity
    {
        long Id { get; set; }
        long WFWorkflowId { get; set; }
        string Name { get; set; }
        WFEntityTypeEnum EntityType { get; set; }

        IWFWorkflow WFWorkflow { get; set; }
    }
}
