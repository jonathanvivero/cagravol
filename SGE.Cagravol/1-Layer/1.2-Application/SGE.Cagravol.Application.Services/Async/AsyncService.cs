using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Async
{
    public class AsyncService 
        : IAsyncService
    {

        private readonly TaskFactory _myTaskFactory;
        public AsyncService()
        {
            _myTaskFactory = new
                TaskFactory(CancellationToken.None,
                    TaskCreationOptions.None,
                    TaskContinuationOptions.None,
                    TaskScheduler.Default);
        }

        public TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return this._myTaskFactory
                .StartNew<Task<TResult>>(func)
                .Unwrap<TResult>()
                .GetAwaiter()
                .GetResult();
        }

        public void RunSync(Func<Task> func)
        {
            this._myTaskFactory
                .StartNew<Task>(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }
    }
}
