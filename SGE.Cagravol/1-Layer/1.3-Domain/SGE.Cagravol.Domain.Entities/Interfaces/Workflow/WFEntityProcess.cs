using SGE.Cagravol.Application.Core.Enums.Common;
using SGE.Cagravol.Application.Core.Enums.Workflows;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public abstract class WFEntityProcess<T>
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
        public abstract IResultModel ExecuteProcess();
        
        public abstract IResultModel ExecuteProcess(T entity);        
    }
}
