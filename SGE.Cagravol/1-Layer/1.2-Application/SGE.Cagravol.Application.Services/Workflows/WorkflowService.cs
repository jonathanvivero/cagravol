using SGE.Cagravol.Application.Services.Common;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Files;
//using SGE.Cagravol.Domain.Repositories.History;
using SGE.Cagravol.Domain.Repositories.Workflows;
using SGE.Cagravol.Presentation.Resources.Customers;
using SGE.Cagravol.Presentation.Resources.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Repositories.History;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Application.Core.Enums.Grafidec;
using SGE.Cagravol.Presentation.Resources.Templates;
using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Repositories.Customers;

namespace SGE.Cagravol.Application.Services.Workflows
{
    public class WorkflowService
        : IWorkflowService
    {
        private readonly IFileRepository fileRepository;
        private readonly IAppConfigurationService appConfigurationService;
        private readonly IWFFileStateRepository wfFileStateRepository;
        private readonly IWorkflowRepository workflowRepository;
        private readonly IWFStateRepository wfStateRepository;
        private readonly IWFTransitionRepository wfTransitionRepository;
        private readonly ICustomerRepository customerRepository;

        public WorkflowService(IFileRepository fileRepository,
            IWFFileStateRepository wfFileStateRepository,
            IWorkflowRepository workflowRepository,
            IWFStateRepository wfStateRepository,
            IWFTransitionRepository wfTransitionRepository,
            IAppConfigurationService appConfigurationService,
            ICustomerRepository customerRepository
            )
        {
            this.appConfigurationService = appConfigurationService;
            this.fileRepository = fileRepository;
            this.wfFileStateRepository = wfFileStateRepository;
            this.workflowRepository = workflowRepository;
            this.wfStateRepository = wfStateRepository;
            this.wfTransitionRepository = wfTransitionRepository;
            this.customerRepository = customerRepository;
        }

        public IResultServiceModel<WFFileState> SetInitialStateForFile(File file, string userId)
        {
            IResultServiceModel<WFFileState> rm = new ResultServiceModel<WFFileState>();
            var now = DateTime.Now;
            WFWorkflow workflow = null;
            string wfCode = this.appConfigurationService.DefaultFileWorkflowCode;

            //Get the customer for the file before to find out what Workflow it needs
            if (file.WFWorkflow != null)
            {
                workflow = file.WFWorkflow;
                wfCode = workflow.Code;
            }
            else if (file.WFWorkflowId.HasValue)
            {
                var rmWF = this.GetWorkflowById(file.WFWorkflowId.Value);
                if (rmWF.Success)
                {
                    workflow = rmWF.Value;
                }
                else
                {
                    workflow = null;
                }
            }

            if (workflow == null)
            {
                var rmWF = this.GetWorkflowByCode(wfCode);
                if (rmWF.Success)
                {
                    workflow = rmWF.Value;
                }
                else
                {
                    workflow = null;
                }
            }

            var rmIS = this.wfStateRepository.GetInitialStateByWorkflowCode(wfCode);
            if (rmIS.Success)
            {
                var state = rmIS.Value;
                if (workflow == null)
                { 
                    workflow = state.WFWorkflows.Where(w=>w.WFWorkflow.Code == wfCode).Select(w=>w.WFWorkflow).FirstOrDefault();
                }

                //put the workflow id and version to the file;
                if (workflow != null)
                {
                    file.WFWorkflowId = workflow.Id;
                    file.WFWorkflowVersion = workflow.WFWorkflowVersion;
                    this.fileRepository.Update(file);
                    this.fileRepository.Save();
                }
                

                var fHis = new WFFileState()
                {
                    EntityId = file.Id,
                    UserId = userId,
                    WFStateId = rmIS.Value.Id,
                    TS = now
                };

                fHis.Notes.Add(new WFFileStateNote() { UserId = userId, TS = now, Comment = state.Name  });

                this.wfFileStateRepository.Add(fHis);
                var rmSave = this.wfFileStateRepository.Save();

                if (rmSave.Success)
                {
                    rm = this.wfFileStateRepository.GetById(fHis.Id);
                    //rm.OnSuccess(state);
                }
                else
                {
                    rm.OnError(rmSave.ErrorMessage, rmSave.ErrorCode);
                }
            }
            else
            {
                rm.OnError(rmIS.ErrorMessage, rmIS.ErrorCode);
            }

            return rm;
        }
        public IResultServiceModel<WFFileState> SetStateForFile(long fileId, string userId, string wfStateCode, string userComment = null)
        {
            IResultServiceModel<WFFileState> rm = new ResultServiceModel<WFFileState>();
            var now = DateTime.Now;

            //Get Default Workflow For Files
            var wfCode = this.appConfigurationService.DefaultFileWorkflowCode;

            var rmIS = this.wfStateRepository.GetSpecificStateByCode(wfCode, wfStateCode);
            if (rmIS.Success)
            {
                var state = rmIS.Value;
                var fHis = new WFFileState()
                {
                    EntityId = fileId,
                    UserId = userId,
                    WFStateId = rmIS.Value.Id,
                    TS = now
                };

                if (string.IsNullOrEmpty(userComment))
                {
                    userComment = state.Name;
                }

                fHis.Notes.Add(new WFFileStateNote() { UserId = userId, TS = now, Comment = userComment });

                this.wfFileStateRepository.Add(fHis);
                var rmSave = this.wfFileStateRepository.Save();

                if (rmSave.Success)
                {
                    rm = this.wfFileStateRepository.GetById(fHis.Id);
                }
                else
                {
                    rm.OnError(rmSave.ErrorMessage, rmSave.ErrorCode);
                }
            }
            else
            {
                rm.OnError(rmIS.ErrorMessage, rmIS.ErrorCode);
            }

            return rm;
        }
        
        public IResultModel AutoMoveAheadStateForFile(long fileId, string userId)
        {
            IResultModel rm = new ResultModel();           
            return rm;
        }
        public IResultModel AutoMoveAheadStateForFile(File file, string userId)
        {
            IResultModel rm = new ResultModel();

            return rm;        
        }       

        public IResultServiceModel<WFState> GetStateByCode(GrafidecWorkflowCodeEnum wfCode, GrafidecStateEnum wfStateCode)
        {
            return this.wfStateRepository.GetSpecificStateByCode(wfCode.ToString(), wfStateCode.ToString());
        }

        public IResultServiceModel<WFState> CheckForNextFileWFMovementByWFCode(long wfWorkflowId, long wfWorkflowVersion, long wfCurrentStateId, string movementCode)
        {
            var rmItem = this.wfStateRepository.GetStateByWorkflowAndCode(wfWorkflowId, wfWorkflowVersion, movementCode);

            if (rmItem.Success)
            {
                //check if between the current state and the next state there is a connection (transition)
                var rmTran = this.wfTransitionRepository.FindByWorkflowAndEndPointStates(wfCurrentStateId, rmItem.Value.Id, wfWorkflowId, wfWorkflowVersion);
                if (rmTran.Success)
                {
                    //Continue successfully;
                }
                else 
                {
                    rmItem.OnError(rmTran.ToResultModel());
                }
            }            
            
            return rmItem;
        }

        public IResultServiceModel<long> GetWorkflowIdByCode(string code)
        {
            return this.workflowRepository.GetWorkflowIdByCode(code);
        }
        public IResultServiceModel<WFWorkflow> GetWorkflowByCode(string code)
        {
            return this.workflowRepository.GetWorkflowByCode(code);
        }
        public IResultServiceModel<WFWorkflow> GetWorkflowById(long id)
        {
            return this.workflowRepository.GetById(id);
        }
    }
}
