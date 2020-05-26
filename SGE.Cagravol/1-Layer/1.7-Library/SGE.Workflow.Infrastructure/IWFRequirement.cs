using SGE.Cagravol.Application.Core.Enums.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFRequirement
    {
        long Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        WFEntityTypeEnum EntityType { get; set; }

        //bool ItPass();
    }
}
