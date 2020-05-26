using SGE.Cagravol.Application.Core.Enums.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFRequirement
    {
        long Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Code { get; set; }
        WFEntityTypeEnum EntityType { get; set; }        
    }
}
