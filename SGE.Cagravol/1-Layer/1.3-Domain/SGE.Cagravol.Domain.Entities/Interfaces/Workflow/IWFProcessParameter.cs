using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFProcessParameter<T, TValue> 
        where TValue : IWFProcessParameterType<object>
    {

        long Id { get; set; }
        IWFProcessParameterType<TValue> Parameter { get;set;}        
    }

    public interface IWFProcessParameterType<T>
    {
        string FieldName { get; set; }
        T Value { get; set; }

    }

}
