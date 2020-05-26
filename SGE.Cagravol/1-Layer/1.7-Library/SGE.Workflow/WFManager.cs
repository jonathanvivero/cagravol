using SGE.Cagravol.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Workflow
{
    public class WFManager<T>
        :IWFManager<T>
        where T: IEntityIdentity
    {
        protected IWFEngine<T> wfEngine { get; set; }

        public WFManager(IWFEngine<T> wfEngine)
        {
            this.wfEngine = wfEngine;            
        }
        public IWFEngine<T> GetWorkflowEngine()
        {
            return this.wfEngine;
        }
    }
}
