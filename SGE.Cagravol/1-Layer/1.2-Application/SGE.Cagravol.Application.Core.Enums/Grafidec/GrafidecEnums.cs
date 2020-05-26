using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.Enums.Grafidec
{
    public enum GrafidecWorkflowCodeEnum
    {
        WORKFLOW_GENERAL,
        WORKFLOW_STANDS
    }

    public enum GrafidecStateEnum
    {
        FILE_IN_UPLOAD,
        FILE_UPLOAD_FAILED,
        FILE_LOADED,
        FILE_RE_UPLOADED,
        FILE_REJECTED,
        FILE_IN_REVISION,
        FILE_READY_FOR_PRODUCTION,
        FILE_IN_PRODUCTION,
        FILE_FILED
    }

    public enum GrafidecTransitionEnum
    {
        FILE_IN_UPLOAD_TO_FILE_UPLOAD_FAILED,
        FILE_IN_UPLOAD_TO_FILE_LOADED,        
        FILE_LOADED_TO_FILE_IN_REVISION,
        FILE_RE_UPLOADED_TO_FILE_IN_REVISION,
        FILE_IN_REVISION_TO_FILE_REJECTED,
        FILE_IN_REVISION_TO_FILE_READY_FOR_PRODUCTION,        
        FILE_READY_FOR_PRODUCTION_TO_FILE_REJECTED,
        FILE_READY_FOR_PRODUCTION_TO_FILE_IN_PRODUCTION,        
        FILE_IN_PRODUCTION_TO_FILE_REJECTED,
        FILE_IN_PRODUCTION_TO_FILE_FILED,               
        FILE_REJECTED_TO_FILE_RE_UPLOADED,

        FILE_LOADED_TO_FILE_IN_PRODUCTION
    }


}
