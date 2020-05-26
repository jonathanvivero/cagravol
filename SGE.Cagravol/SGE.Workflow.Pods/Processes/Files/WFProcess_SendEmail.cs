using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;

namespace SGE.Workflow.Pods
{
    public class WFProcess_SendEmail
        : WFEntityProcess<File>
    {
        
        public WFProcess_SendEmail()
            :base()
        {            
        }
        public WFProcess_SendEmail(File entity)
            : base(entity)
        {            
        }

        public override IResultModel ExecuteProcess() {
            IResultModel rm = new ResultModel();

            return rm;
        }

        public override IResultModel ExecuteProcess(File entity)
        {
            IResultModel rm = new ResultModel();

            return rm;
        }
    }
}
