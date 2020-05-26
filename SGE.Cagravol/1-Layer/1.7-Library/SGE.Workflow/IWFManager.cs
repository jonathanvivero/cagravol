using SGE.Cagravol.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Workflow
{
    public interface IWFManager<T>
        where T : IEntityIdentity
    {
        IWFEngine<T> GetWorkflowEngine();
    }
}
