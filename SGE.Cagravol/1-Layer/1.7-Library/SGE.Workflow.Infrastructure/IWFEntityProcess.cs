using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFEntityProcess<T>
        : IWFProcess
    {
        // => Comes from IWFProcess
        //long Id { get; set; }
        //string Name { get; set; }
        //string Description { get; set; }
        //string Code { get; set; }
        //WFEntityTypeEnum EntityType { get; set; }
        T Entity { get; set; }        
        IResultModel ExecuteProcess(T entity);

    }
}
