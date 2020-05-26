using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;

namespace SGE.Workflow.Pods.Files
{
    public class WFTransition_FileUploaded
        : WFEntityTransition<File>
    {        

        public WFTransition_FileUploaded()
            :base()
        {
            IWFEntityProcess<File> wfProc;

            wfProc = new WFProcess_SendEmail();
            this.EntityWFProcesses.Add(wfProc);
        }


    }
}
