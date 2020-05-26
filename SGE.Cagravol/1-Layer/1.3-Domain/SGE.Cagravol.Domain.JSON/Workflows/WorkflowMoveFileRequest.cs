using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Workflows
{
    public class WorkflowMoveFileRequest
    {
        public long fileId { get; set; }
        public long version { get; set; }
        public string movementCode { get; set; }
        public long wfCurrentStateId { get; set; }
        public string comment { get; set; }
        public long customerId { get; set; }
        public string userName { get; set; }
    }
}
