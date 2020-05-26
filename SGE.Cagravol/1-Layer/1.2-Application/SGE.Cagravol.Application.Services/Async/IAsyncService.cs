using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Async
{
    public interface IAsyncService
    {

        TResult RunSync<TResult>(Func<Task<TResult>> func);
        void RunSync(Func<Task> func);
    }
}
