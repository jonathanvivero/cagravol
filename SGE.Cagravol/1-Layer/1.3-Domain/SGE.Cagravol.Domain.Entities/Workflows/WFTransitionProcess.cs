using SGE.Cagravol.Application.Core.Enums.Workflows;
using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Workflows
{
    public class WFTransitionProcess
        : IEntityNoAutoIdentity, IWFTransitionProcess
    {
        public string Id { get; set; }
        public long WFTransitionId { get; set; }
        public long WFProcessId { get; set; }

        public virtual WFTransition WFTransition { get; set; }
        public virtual WFProcess WFProcess { get; set; }        
    }
}
