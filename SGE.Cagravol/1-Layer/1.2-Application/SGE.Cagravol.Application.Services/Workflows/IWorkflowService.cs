using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Application.Core.Enums.Grafidec;
using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Domain.Entities.History;

namespace SGE.Cagravol.Application.Services.Workflows
{
    public interface IWorkflowService
    {
        /// <summary>
        /// Set the initial state for the file.
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IResultServiceModel<WFFileState> SetInitialStateForFile(File file, string userId);
        /// <summary>
        /// Move the state of the file to the step of provided code
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="userId"></param>
        /// <param name="wfStateCode"></param>
        /// <returns></returns>
        IResultServiceModel<WFFileState> SetStateForFile(long fileId, string userId, string wfStateCode, string userComment = null);        
        
        /// <summary>
        /// Auto Move ahead the state of the file. If the file has more than one posibilities, it does not move and return unsuccess.
        /// </summary>
        /// <param name="fileId">File Identification </param>
        /// <param name="userId"> current user in session Id</param>
        /// <returns>Success if moved. Unsuccess if not moved.</returns>
        IResultModel AutoMoveAheadStateForFile(long fileId, string userId);

        /// <summary>
        /// Auto Move ahead the state of the file. If the file has more than one posibilities, it does not move and return unsuccess.
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="userId">current user in session</param>
        /// <returns>Success if moved. Unsuccess if not moved Id</returns>
        IResultModel AutoMoveAheadStateForFile(File file, string userId);
        IResultServiceModel<WFState> GetStateByCode(GrafidecWorkflowCodeEnum wfCode, GrafidecStateEnum wfStateCode);                

        /// <summary>
        /// For the current Worflow, check if is able to move the Parameter-CODE workflow state by
        /// - checking if a state with the Parameter-CODE exist for the Paremeter-WORKFLOW
        /// - Checking if there is a transition from the Parameter-CurrentState to the found state
        /// </summary>
        /// <param name="wfWorkflowId"></param>
        /// <param name="wfWorkflowVersion"></param>
        /// <param name="wfCurrentStateId"></param>
        /// <param name="movementCode"></param>
        /// <returns></returns>
        IResultServiceModel<WFState> CheckForNextFileWFMovementByWFCode(long wfWorkflowId, long wfWorkflowVersion, long wfCurrentStateId, string movementCode);

        IResultServiceModel<long> GetWorkflowIdByCode(string code);
        IResultServiceModel<WFWorkflow> GetWorkflowByCode(string code);
        IResultServiceModel<WFWorkflow> GetWorkflowById(long id);
    }
}
