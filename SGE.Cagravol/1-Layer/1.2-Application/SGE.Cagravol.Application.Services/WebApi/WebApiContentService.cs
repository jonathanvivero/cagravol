using SGE.Cagravol.Application.Core.Helpers;
using SGE.Cagravol.Application.Services.Async;
using SGE.Cagravol.Application.Services.Common;
using SGE.Cagravol.Application.Services.Identity;
using SGE.Cagravol.Application.Services.Logs;
using SGE.Cagravol.Application.Services.Utils;
using SGE.Cagravol.Application.Services.Workflows;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.JSON.Files;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Domain.JSON.Account;
using SGE.Cagravol.Domain.JSON.Alfred;
using SGE.Cagravol.Domain.JSON.Files;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.POCO.ServiceModels.Email;
using SGE.Cagravol.Domain.POCO.ServiceModels.Identity;
using SGE.Cagravol.Domain.Repositories.Common;
using SGE.Cagravol.Domain.Repositories.Customers;
using SGE.Cagravol.Domain.Repositories.Files;
using SGE.Cagravol.Domain.Repositories.Projects;
using SGE.Cagravol.Infrastructure.Utils.Definitions;
using SGE.Cagravol.Presentation.Resources.Common;
using SGE.Cagravol.Presentation.Resources.Customers;
using SGE.Cagravol.Presentation.Resources.Templates;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Application.Core.Enums.Common;
using SGE.Cagravol.Application.Core.Enums.Grafidec;
using SGE.Cagravol.Application.Services.Mailing;
using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Application.Services.History;
using SGE.Cagravol.Domain.POCO.Customers;
using SGE.Cagravol.Domain.JSON.Workflows;
using SGE.Cagravol.Domain.POCO.Workflows;
using SGE.Cagravol.Domain.POCO.Files;
using System.Web.Hosting;
using SGE.Cagravol.Domain.JSON.Projects;

namespace SGE.Cagravol.Application.Services.WebApi
{
    public class WebApiContentService : IWebApiContentService
    {
        private readonly ISessionStorageService sessionService;
        private readonly IHtmlToViewHelper htmlToViewHelper;
        private readonly IUtilService utilService;
        private readonly IAccountService accountService;
        private readonly ILogService logService;
        private readonly ICustomerRepository customerRepository;
        private readonly IAsyncService asyncService;
        private readonly IAppConfigurationRepository appConfigurationRepository;
        private readonly IAppConfigurationService appConfigurationService;
        private readonly IProjectRepository projectRepository;
        private readonly IFileService fileService;
        private readonly IFileUploadRepository fileUploadRepository;
        private readonly IFileRepository fileRepository;
        private readonly IFileTypeRepository fileTypeRepository;
        private readonly IWorkflowService workflowService;
        private readonly IMailingService mailingService;
        private readonly IHistoryService historyService;

        #region Public Properties
        public ISessionStorageService SessionService
        {
            get
            {
                return this.sessionService;
            }
        }
        public IHtmlToViewHelper HtmlToViewHelper
        {
            get
            {
                return this.htmlToViewHelper;
            }
        }
        public IUtilService UtilService
        {
            get
            {
                return this.utilService;
            }
        }
        public IAccountService AccountService
        {
            get
            {
                return this.accountService;
            }
        }
        public ILogService LogService
        {
            get
            {
                return this.logService;
            }
        }
        #endregion

        public WebApiContentService(ISessionStorageService sessionService,
            IHtmlToViewHelper htmlToViewHelper,
            IUtilService utilService,
            IAccountService accountService,
            ILogService logService,
            IAsyncService asyncService,
            IAppConfigurationRepository appConfigurationRepository,
            ICustomerRepository customerRepository,
            IProjectRepository projectRepository,
            IFileService fileService,
            IFileUploadRepository fileUploadRepository,
            IFileRepository fileRepository,
            IFileTypeRepository fileTypeRepository,
            IWorkflowService workflowService,
            IMailingService mailingService,
            IHistoryService historyService,
            IAppConfigurationService appConfigurationService
            )
        {
            this.appConfigurationService = appConfigurationService;
            this.mailingService = mailingService;
            this.fileRepository = fileRepository;
            this.sessionService = sessionService;
            this.htmlToViewHelper = htmlToViewHelper;
            this.utilService = utilService;
            this.accountService = accountService;
            this.logService = logService;
            this.customerRepository = customerRepository;
            this.asyncService = asyncService;
            this.appConfigurationRepository = appConfigurationRepository;
            this.projectRepository = projectRepository;
            this.fileService = fileService;
            this.fileUploadRepository = fileUploadRepository;
            this.fileTypeRepository = fileTypeRepository;
            this.workflowService = workflowService;
            this.historyService = historyService;
        }

        /// <summary>
        /// Register a new Customer in a Project, with the request parameters
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IResultServiceModel<Customer>> RegisterNewCustomerInProjectAsync(CustomerRegistrationRequest request, bool mustBeFree = true)
        {
            return await Task.FromResult(RegisterNewCustomerInProject(request, mustBeFree));
        }
        public IResultServiceModel<Customer> RegisterNewCustomerInProject(CustomerRegistrationRequest request, bool mustBeFree = true)
        {
            IResultServiceModel<Customer> rsm = new ResultServiceModel<Customer>();
            try
            {
                var rsmCustomer = this.customerRepository.GetCustomerbyProjectCode(request.projectCode, mustBeFree);
                if (rsmCustomer.Success)
                {
                    var rvPwd = this.utilService.ReviewPasswordSecurity(request.password, request.confirmPassword);

                    if (rvPwd.Success)
                    {
                        //Asigna este usuario y lo da de alta.
                        var customer = rsmCustomer.Value;
                        customer.Email = request.userName;
                        customer.Name = request.name;
                        customer.PasswordHash = utilService.EncriptarTripleDES(request.password);
                        customer.UserId = "";


                        //Create the user for the customer
                        var user = new User()
                        {
                            Email = request.userName,
                            EmailConfirmed = true,
                            CreatedOn = DateTime.Now,
                            IsActive = true,
                            UpdatedOn = DateTime.Now,
                            LastAccess = DateTime.Now,
                            UserName = request.userName.Replace('-', '_')
                        };

                        //Add the Customer User within the Customer Role
                        UserCreationResultServiceModel ucr = this.asyncService.RunSync<UserCreationResultServiceModel>(() => this.accountService.CreateUserWithMainRoleAsync(user, request.password, RoleDefinitions.Customer));
                        if (ucr.Succeeded)
                        {
                            customer.UserId = ucr.UserCreated.Id;
                            customer.Registered = true;
                            customer.Reserved = true;

                            this.customerRepository.Update(customer);
                            this.customerRepository.Save();

                            rsm.OnSuccess(customer);
                        }
                        else
                        {
                            rsm.OnError(ucr.FailMessage, "");
                        }
                    }
                    else
                    {
                        rsm.OnError(rvPwd.ErrorMessage, rvPwd.ErrorCode);
                    }
                }
                else
                {
                    rsm.OnError(rsmCustomer.ErrorMessage, rsmCustomer.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public async Task<IResultServiceModel<UserLoginResponse>> UserLoginAsync(UserLoginRequest request)
        {
            return await Task.FromResult(UserLogin(request));
        }
        public IResultServiceModel<UserLoginResponse> UserLogin(UserLoginRequest request)
        {
            IResultServiceModel<UserLoginResponse> rsm = new ResultServiceModel<UserLoginResponse>();

            rsm.OnError(CommonResources.NotImplemented);

            return rsm;
        }
        public IResultModel SendEmailNewCustomerSignUp(Customer customer, string hostAddress)
        {

            return this.mailingService.SendEmailNewCustomerSignUp(customer, hostAddress);
        }
        public IResultModel SendTestEmail(string email, string message)
        {
            return this.mailingService.SendTestEmail(email, message);
        }
        public IResultServiceModel<WorkflowMoveFileResponse> MoveWorkflowStateForFile(WorkflowMoveFileRequest request)
        {
            return this.asyncService.RunSync(() => this.MoveWorkflowStateForFileAsync(request));
        }
        public async Task<IResultServiceModel<WorkflowMoveFileResponse>> MoveWorkflowStateForFileAsync(WorkflowMoveFileRequest request)
        {
            IResultServiceModel<WorkflowMoveFileResponse> rsm = new ResultServiceModel<WorkflowMoveFileResponse>();
            try
            {
                var user = await this.accountService.FindUserByUserNameAsync(request.userName);
                if (user == null)
                {
                    return rsm.OnError(ErrorResources.UserHasNotAuthorizationToPerformThisAction, EnumErrorCode.INSUFFICIENT_PRIVILEGES);
                }

                var isAuthorized = this.accountService.IsUserInRoleById(user.Id, RoleDefinitions.Organizer);
                if (isAuthorized)
                {
                    //Get the file
                    var rmFile = this.fileRepository.Find(request.fileId);
                    if (rmFile.Success)
                    {
                        var file = rmFile.Value;

                        if (file.WFCurrentEntityStateId.HasValue && file.WFWorkflowId.HasValue && file.WFWorkflowVersion.HasValue)
                        {


                            //Check if the workflowstate is correct.
                            if (file.WFCurrentEntityState.WFStateId == request.wfCurrentStateId)
                            {
                                //check the movement (by code) exists for the file workflow

                                var rmNextState = this.workflowService.CheckForNextFileWFMovementByWFCode(file.WFWorkflowId.Value, file.WFWorkflowVersion.Value, file.WFCurrentStateId.Value, request.movementCode);
                                if (rmNextState.Success)
                                {
                                    var rmMovement = this.workflowService.SetStateForFile(file.Id, user.Id, request.movementCode, request.comment);



                                    if (rmMovement.Success)
                                    {
                                        rsm.OnSuccess(
                                            new WorkflowMoveFileResponse()
                                            {
                                                wfCurrentState = new WFFileStatePOCO(rmMovement.Value)
                                            }
                                        );
                                    }
                                    else
                                    {
                                        rsm.OnError(rmMovement.ErrorMessage, rmMovement.ErrorCode);
                                    }
                                }
                                else
                                {
                                    rsm.OnError(rmNextState.ErrorMessage, rmNextState.ErrorCode);
                                }
                            }
                            else
                            {
                                rsm.OnError(ErrorResources.FileWFStateIsNotInThisStateAnyMore, EnumErrorCode.FILE_WF_STATE_DOES_NOT_MATCH);
                            }
                        }
                        else
                        {
                            rsm.OnError(ErrorResources.FileWFStateIsNotInThisStateAnyMore, EnumErrorCode.FILE_WF_STATE_DOES_NOT_MATCH);
                        }
                    }
                    else
                    {
                        rsm.OnError(ErrorResources.FileNotFound, EnumErrorCode.FILE_NOT_FOUND);
                    }
                }
                else
                {
                    rsm.OnError(ErrorResources.UserHasNotAuthorizationToPerformThisAction, EnumErrorCode.INSUFFICIENT_PRIVILEGES);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultServiceModel<AlfredDefaultPlatformParametersResponse> GetDefaultPlatformParameters()
        {
            IResultServiceModel<AlfredDefaultPlatformParametersResponse> rsm = new ResultServiceModel<AlfredDefaultPlatformParametersResponse>();
            var alfred = new AlfredDefaultPlatformParametersResponse();

            try
            {
                var rsmList = this.appConfigurationRepository.GetOnlyPlatformParams();
                if (rsmList.Success)
                {
                    foreach (var item in rsmList.Value)
                    {
                        switch (item.Key)
                        {
                            case EnumAppConfigurationKeyDefinition.DefaultTotalStandsPerEvent:
                                int ts = 0;
                                if (int.TryParse(item.Value, out ts))
                                {
                                    alfred.totalStands = ts;
                                }
                                else
                                {
                                    alfred.totalStands = 1;
                                }
                                break;
                            case EnumAppConfigurationKeyDefinition.PublicKey:
                                alfred.publicKey = item.Value;
                                break;
                            default:
                                break;
                        }
                    }

                    //File Types
                    var rsmFileTypes = this.fileTypeRepository.GetAllJSON();
                    if (rsmFileTypes.Success)
                    {
                        alfred.fileTypes = rsmFileTypes.Value.ToArray();
                    }
                    else
                    {
                        alfred.fileTypes = new FileTypeJSON[] { };
                    }


                    rsm.OnSuccess(alfred);
                }
                else
                {
                    rsm.OnError(rsmList.ErrorMessage, rsmList.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public async Task<IResultServiceModel<AlfredDefaultPlatformParametersResponse>> GetDefaultPlatformParametersAsync()
        {
            return await Task.FromResult(this.GetDefaultPlatformParameters());
        }
        public IResultServiceModel<Customer> CheckProjectForLoggedCustomer(string userId)
        {
            IResultServiceModel<Customer> rsm = new ResultServiceModel<Customer>();

            try
            {
                var rsmCuz = this.customerRepository.FindByUserId(userId);
                if (rsmCuz.Success)
                {
                    var rsmProject = this.projectRepository.Find(rsmCuz.Value.ProjectId);
                    if (rsmProject.Success)
                    {
                        rsm = rsmCuz;
                    }
                    else
                    {
                        rsm.OnError(rsmProject.ErrorMessage, rsmProject.ErrorCode);
                    }
                }
                else
                {
                    rsm.OnError(rsmCuz.ErrorMessage, rsmCuz.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public async Task<IResultServiceModel<Customer>> CheckProjectForLoggedCustomerByName(string userName)
        {
            IResultServiceModel<Customer> rsm = new ResultServiceModel<Customer>();

            try
            {
                var user = await this.accountService.FindUserByUserNameOrEmailAsync(userName);

                var rsmCuz = this.customerRepository.FindByUserId(user.Id);
                if (rsmCuz.Success)
                {
                    var rsmProject = this.projectRepository.GetById(rsmCuz.Value.ProjectId);
                    if (rsmProject.Success)
                    {
                        rsm = rsmCuz;
                    }
                    else
                    {
                        rsm.OnError(rsmProject.ErrorMessage, rsmProject.ErrorCode);
                    }
                }
                else
                {
                    rsm.OnError(rsmCuz.ErrorMessage, rsmCuz.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public async Task<IResultServiceModel<FileUploadResponse>> FileUploadInitAsync(FileUploadRequest request)
        {
            IResultServiceModel<FileUploadResponse> rsm = new ResultServiceModel<FileUploadResponse>();
            User user = null;
            FileUploadResponse fur = new FileUploadResponse();
            FileUpload fu = new FileUpload();

            try
            {
                this.fileService.EnsureDirectoryExists(UploadFoldersEnum.Tmp);
                this.fileService.EnsureDirectoryExists(UploadFoldersEnum.CustomerFiles);

                if (!string.IsNullOrWhiteSpace(request.channelId) && request.channelId.Length > 32)
                {
                    fur.channelId = request.channelId;
                }
                else
                {
                    fur.channelId = this.utilService.GetGUID();
                }

                fur.index = 0;

                var rootPath = this.fileService.GetPhysicalPath(UploadFoldersEnum.Tmp.ToString());
                var tmpFolder = this.fileService.CreateFolder(rootPath, fur.channelId);
                var filePath = System.IO.Path.Combine(UploadFoldersEnum.Tmp.ToString(), fur.channelId, "index");
                //create an empty file for testing purposes
                var res = this.fileService.Save(filePath, new byte[] { });

                user = await this.accountService.FindUserByUserNameOrEmailAsync(request.userName);
                var rmCuz = this.customerRepository.FindByUserId(user.Id);

                if (!rmCuz.Success)
                {
                    if (this.accountService.IsUserInRoleById(user.Id, RoleDefinitions.Organizer))
                    {
                        rmCuz = this.customerRepository.GetGeneralSpaceCustomerByProject(request.projectId);
                    }
                }

                if (rmCuz.Success)
                {
                    fu.ChannelId = fur.channelId;
                    fu.CustomerId = rmCuz.Value.Id;
                    fu.CustomerLogicalName = request.customerLogicalName;
                    fu.LastHashUploadDate = DateTime.Now;
                    fu.StartDate = DateTime.Now;
                    fu.MimeType = request.mimeType;
                    fu.FileTypeId = request.fileTypeId;
                    fu.Name = request.fileName;
                    fu.IsCompleted = false;
                    fu.PartsCounter = 0;
                    fu.PartsTotal = request.parts;
                    fu.Size = request.size;
                    fu.TempFolder = tmpFolder;
                    fu.FileNotes = request.fileNotes ?? string.Empty;
                    var partsMapLen = string.Format("1{{0:D{0}}}", request.parts);
                    partsMapLen = "1" + new String('0', (int)request.parts);
                    fu.UploadPartsMapCode = string.Format(partsMapLen, 0);
                }
                else
                {
                    rsm.OnError(rmCuz.ErrorMessage, rmCuz.ErrorCode);
                }

                this.fileUploadRepository.Add(fu);
                var rmSaveFU = this.fileUploadRepository.Save();

                if (rmSaveFU.Success)
                {
                    rsm.OnSuccess(fur);
                }
                else
                {
                    rsm.OnError(rmSaveFU.ErrorMessage, rmSaveFU.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;

        }
        public IResultServiceModel<FileUploadResponse> FileUploadInit(FileUploadRequest request)
        {
            return this.asyncService.RunSync(() => this.FileUploadInitAsync(request));
        }
        public async Task<IResultServiceModel<FileHashUploadResponse>> UploadFileHashAsync(FileHashUploadRequest request)
        {
            return await Task.FromResult(this.UploadFileHash(request));
        }
        public IResultServiceModel<FileHashUploadResponse> UploadFileHash(FileHashUploadRequest request)
        {
            IResultServiceModel<FileHashUploadResponse> rsm = new ResultServiceModel<FileHashUploadResponse>();
            FileHashUploadResponse fur = new FileHashUploadResponse();

            try
            {
                var rmFU = this.fileUploadRepository.FindByChannel(request.channelId);

                if (rmFU.Success)
                {
                    fur.channelId = request.channelId;
                    fur.index = request.index;

                    var fu = rmFU.Value;
                    var fileId = string.Format("{0:000000}", fur.index);
                    var filePath = System.IO.Path.Combine(UploadFoldersEnum.Tmp.ToString(), fur.channelId, fileId);

                    var res = this.fileService.Save(filePath, Convert.FromBase64String(request.hash));

                    char[] map = fu.UploadPartsMapCode.ToCharArray();
                    map[request.index] = '1';

                    fu.LastHashUploadDate = DateTime.Now;
                    fu.PartsCounter = fu.PartsCounter + 1;
                    fu.UploadPartsMapCode = new string(map);

                    this.fileUploadRepository.Update(fu);
                    var rmSave = this.fileUploadRepository.Save();

                    if (rmSave.Success)
                    {
                        rsm.OnSuccess(fur);
                    }
                    else
                    {
                        rsm.OnError(rmSave.ErrorMessage, rmSave.ErrorCode);
                    }
                }
                else
                {
                    rsm.OnError(rmFU.ErrorMessage, rmFU.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultServiceModel<FileEndUploadResponse> EndFileUpload(FileEndUploadRequest request, string hostRoot)
        {
            //return await Task.FromResult(this.EndFileUpload(request, hostRoot));
            return this.asyncService.RunSync(() => this.EndFileUploadAsync(request, hostRoot));
        }
        public IResultServiceModel<FileEndUploadResponse> EndFileResendUpload(FileEndUploadRequest request, string hostRoot)
        {
            //return await Task.FromResult(this.EndFileUpload(request, hostRoot));
            return this.asyncService.RunSync(() => this.EndFileResendUploadAsync(request, hostRoot));
        }
        public async Task<IResultServiceModel<FileEndUploadResponse>> EndFileUploadAsync(FileEndUploadRequest request, string hostRoot)
        {
            IResultServiceModel<FileEndUploadResponse> rsm = new ResultServiceModel<FileEndUploadResponse>();
            FileEndUploadResponse fur = new FileEndUploadResponse();
            User user = await this.accountService.FindUserByUserNameOrEmailAsync(request.userName);

            try
            {
                var rmFU = this.fileUploadRepository.FindByChannel(request.channelId);

                if (rmFU.Success)
                {
                    fur.channelId = request.channelId;

                    var fu = rmFU.Value;

                    fu.IsCompleted = true;
                    this.fileUploadRepository.Update(fu);
                    var rmSave = this.fileUploadRepository.Save();

                    if (rmSave.Success)
                    {
                        var rmMoveFile = this.MoveHashesToCustomerArea(fu, hostRoot);
                        if (rmMoveFile.Success)
                        {

                            fur.url = rmMoveFile.Value.URL;
                            File file = rmMoveFile.Value;
                            ///TODO: Add WorkFlowState Initial
                            ///
                            var rmSetState = this.workflowService.SetInitialStateForFile(file, user.Id);

                            if (file.WFWorkflow == null && file.WFWorkflowId.HasValue)
                            {
                                var rmWF = this.workflowService.GetWorkflowById(file.WFWorkflowId.Value);
                                if (rmWF.Success)
                                {
                                    file.WFWorkflow = rmWF.Value;
                                }
                            }

                            //Ahora, dependiendo del workflow que tenga
                            if (file.WFWorkflow != null && file.WFWorkflow.Code == this.appConfigurationService.DefaultGeneralSpaceFileWorkflowCode)
                            {
                                if (rmSetState.Success)
                                {
                                    //Pasa from File Uploaded Successfully to File In Revision
                                    rmSetState = this.workflowService.SetStateForFile(file.Id, user.Id, GrafidecStateEnum.FILE_IN_PRODUCTION.ToString());
                                }
                            }
                            else
                            {
                                if (rmSetState.Success)
                                {
                                    //Pasa from File In Upload to File Uploaded Successfully
                                    rmSetState = this.workflowService.SetStateForFile(file.Id, user.Id, GrafidecStateEnum.FILE_LOADED.ToString());
                                }

                                if (rmSetState.Success)
                                {
                                    //Pasa from File Uploaded Successfully to File In Revision
                                    rmSetState = this.workflowService.SetStateForFile(file.Id, user.Id, GrafidecStateEnum.FILE_IN_REVISION.ToString());
                                }
                            }

                            if (rmSetState.Success)
                            {
                                //Reporta a los usuarios managers
                                var state = rmSetState.Value;
                                rmSetState.OnSuccess(state);

                                //Envía notificaciones a todas las partes, pero no espera por ellos
                                //Envía notificaciones a todas las partes, pero no espera por ellos
                                HostingEnvironment.QueueBackgroundWorkItem(cancellationToken =>
                                {
                                    Task.Run<IResultModel>(() => this.ReportManagerUsersFileStateChanged(file, user, state.WFState, hostRoot));
                                    // Some long-running job
                                });
                                //var reportTask = this.ReportManagerUsersFileStateChangedAsync(file, user, state.WFState, hostRoot);

                                rsm.OnSuccess(fur);
                            }
                            else
                            {
                                rsm.OnError(rmSetState.ErrorMessage, rmSetState.ErrorCode);
                            }


                        }
                        else
                        {
                            rsm.OnError(rmMoveFile.ErrorMessage, rmMoveFile.ErrorCode);
                        }
                    }
                    else
                    {
                        rsm.OnError(rmSave.ErrorMessage, rmSave.ErrorCode);
                    }
                }
                else
                {
                    rsm.OnError(rmFU.ErrorMessage, rmFU.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public async Task<IResultServiceModel<FileEndUploadResponse>> EndFileResendUploadAsync(FileEndUploadRequest request, string hostRoot)
        {
            IResultServiceModel<FileEndUploadResponse> rsm = new ResultServiceModel<FileEndUploadResponse>();
            FileEndUploadResponse fur = new FileEndUploadResponse();
            User user = await this.accountService.FindUserByUserNameOrEmailAsync(request.userName);

            try
            {
                var rmFU = this.fileUploadRepository.FindByChannel(request.channelId);

                if (rmFU.Success)
                {
                    fur.channelId = request.channelId;

                    var fu = rmFU.Value;

                    fu.IsCompleted = true;
                    this.fileUploadRepository.Update(fu);
                    var rmSave = this.fileUploadRepository.Save();

                    if (rmSave.Success)
                    {
                        var rmMoveFile = this.MoveHashesToExistingCustomerArea(fu, request.fileId, hostRoot);
                        if (rmMoveFile.Success)
                        {

                            fur.url = rmMoveFile.Value.URL;
                            File file = rmMoveFile.Value;


                            //Pasa from File In Upload to File RE-Uploaded Successfully
                            var rmSetState = this.workflowService.SetStateForFile(file.Id, user.Id, GrafidecStateEnum.FILE_RE_UPLOADED.ToString());

                            if (file.WFWorkflow == null && file.WFWorkflowId.HasValue)
                            {
                                var rmWF = this.workflowService.GetWorkflowById(file.WFWorkflowId.Value);
                                if (rmWF.Success)
                                {
                                    file.WFWorkflow = rmWF.Value;
                                }
                            }


                            //Ahora, dependiendo del workflow que tenga
                            if (file.WFWorkflow != null && file.WFWorkflow.Code == this.appConfigurationService.DefaultGeneralSpaceFileWorkflowCode)
                            {
                                //GENERAL SPACE WORKFLOW

                                if (rmSetState.Success)
                                {
                                    //Pasa from File Uploaded Successfully to File In Revision
                                    rmSetState = this.workflowService.SetStateForFile(file.Id, user.Id, GrafidecStateEnum.FILE_IN_PRODUCTION.ToString());
                                }
                            }
                            else
                            {
                                //STANDS WORKFLOW 

                                if (rmSetState.Success)
                                {
                                    //Pasa from File Uploaded Successfully to File In Revision
                                    rmSetState = this.workflowService.SetStateForFile(file.Id, user.Id, GrafidecStateEnum.FILE_IN_REVISION.ToString());
                                }
                            }

                            if (rmSetState.Success)
                            {
                                //Reporta a los usuarios managers
                                var state = rmSetState.Value;
                                rmSetState.OnSuccess(state);

                                //Envía notificaciones a todas las partes, pero no espera por ellos
                                HostingEnvironment.QueueBackgroundWorkItem(cancellationToken =>
                                {
                                    Task.Run<IResultModel>(() => this.ReportManagerUsersFileStateChanged(file, user, state.WFState, hostRoot));
                                    // Some long-running job
                                });

                                //var reportTask = this.ReportManagerUsersFileStateChangedAsync(file, user, state.WFState, hostRoot);

                                rsm.OnSuccess(fur);
                            }
                            else
                            {
                                rsm.OnError(rmSetState.ErrorMessage, rmSetState.ErrorCode);
                            }

                        }
                        else
                        {
                            rsm.OnError(rmMoveFile.ErrorMessage, rmMoveFile.ErrorCode);
                        }
                    }
                    else
                    {
                        rsm.OnError(rmSave.ErrorMessage, rmSave.ErrorCode);
                    }
                }
                else
                {
                    rsm.OnError(rmFU.ErrorMessage, rmFU.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultModel FileCancelUpload(FileCancelUploadRequest request)
        {
            IResultModel rm = new ResultModel();

            try
            {
                var rmFU = this.fileUploadRepository.FindByChannel(request.channelId);
                if (rmFU.Success)
                {
                    var fu = rmFU.Value;
                    string[] tempFolders = new string[] { UploadFoldersEnum.Tmp.ToString(), fu.ChannelId };

                    //Remove all Temporary files
                    rm = this.fileUploadRepository.RemoveAllCompleted();
                    if (rm.Success)
                    {
                        rm = this.fileService.DeleteFolderPhysicalPath(tempFolders);
                    }
                }
                else
                {
                    rm.OnError(rmFU.ErrorMessage, rmFU.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }
        public IResultModel UpdateFileForProjectGeneralSpace(long id, string name, long fileTypeId, long projectId, string userName)
        {
            IResultModel rm = new ResultModel();

            try
            {
                var rmFile = this.fileRepository.FindGSByIdAndProject(id, projectId);
                if (rmFile.Success)
                {
                    var file = rmFile.Value;

                    file.Name = name;
                    file.FileTypeId = fileTypeId;

                    this.fileRepository.Update(file);
                    var rmSave = this.fileRepository.Save();

                    if (rmSave.Success)
                    {
                        rm.OnSuccess();
                    }
                    else
                    {
                        rm.OnError(rmSave.ErrorMessage, rmSave.ErrorCode);
                    }
                }
                else
                {
                    rm.OnError(ErrorResources.ItemDoesNotExist, EnumErrorCode.ITEM_DOES_NOT_EXIST);
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }
        public IResultModel UpdateFile(long id, string name, string notes, long fileTypeId, string userName)
        {
            IResultModel rm = new ResultModel();

            try
            {
                var rmFile = this.fileRepository.Find(id);
                if (rmFile.Success)
                {
                    var file = rmFile.Value;

                    file.Name = name;
                    file.FileNotes = notes;
                    file.FileTypeId = fileTypeId;

                    this.fileRepository.Update(file);
                    var rmSave = this.fileRepository.Save();

                    if (rmSave.Success)
                    {
                        rm.OnSuccess();
                    }
                    else
                    {
                        rm.OnError(rmSave.ErrorMessage, rmSave.ErrorCode);
                    }
                }
                else
                {
                    rm.OnError(ErrorResources.ItemDoesNotExist, EnumErrorCode.ITEM_DOES_NOT_EXIST);
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }
        public async Task<IResultServiceModel<FileHistoryResponse>> GetFileHistoryAsync(string userName, long id)
        {
            IResultServiceModel<FileHistoryResponse> response = new ResultServiceModel<FileHistoryResponse>();

            try
            {
                var user = await this.accountService.FindUserByUserNameAsync(userName);
                //Check the roles for the user
                if (user != null)
                {
                    var rmCheckRole = await this.accountService.UserIsOnlyCustomer(user);
                    if (rmCheckRole.Success)
                    {
                        bool isOnlyCustomer = rmCheckRole.Value;
                        //Get the file History, checking the user credentials for this file
                        response = this.historyService.GetFileHistoryByFileAndUser(id, user, isOnlyCustomer);
                    }
                    else
                    {
                        response.OnError(rmCheckRole.ToResultModel());
                    }
                }
                else
                {
                    response.OnError(ErrorResources.UserNoActiveAccount);
                }

            }
            catch (Exception ex)
            {
                response.OnException(ex);
            }

            return response;
        }
        public async Task<IResultServiceModel<FileStateCommentResponse>> AddCommentToFileStateAsync(FileStateCommentRequest request)
        {
            var user = await this.accountService.FindUserByUserNameAsync(request.userName);

            if (user != null)
            {
                return this.historyService.AddCommentToFileState(request, user);
            }
            else
            {
                return new ResultServiceModel<FileStateCommentResponse>().OnError();
            }
        }
        public IResultServiceModel<CustomerPOCO> GetCustomer(long projectId, long customerId, string userName)
        {
            IResultServiceModel<CustomerPOCO> rsm = new ResultServiceModel<CustomerPOCO>();

            try
            {
                var isAuthorized = this.accountService.IsUserInRole(userName, RoleDefinitions.Organizer);

                if (isAuthorized)
                {
                    var rmCuz = this.customerRepository.FindByIdAndProject(customerId, projectId);
                    if (rmCuz.Success)
                    {
                        rsm.OnSuccess(
                            new CustomerPOCO(rmCuz.Value)
                            );


                    }
                    else
                    {
                        rsm.OnError(rmCuz.ToResultModel());
                    }
                }
                else
                {
                    rsm.OnError(ErrorResources.UserHasNotAuthorizationToPerformThisAction, EnumErrorCode.INSUFFICIENT_PRIVILEGES);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultServiceModel<Customer> GetCustomerForProjectGeneralSpace(long projectId, string userName)
        {
            IResultServiceModel<Customer> rsm = new ResultServiceModel<Customer>();

            try
            {
                var isAuthorized = this.accountService.IsUserInRole(userName, RoleDefinitions.Organizer);

                if (isAuthorized)
                {
                    var rmCuz = this.customerRepository.GetGeneralSpaceCustomerByProject(projectId);
                    if (rmCuz.Success)
                    {
                        rsm.OnSuccess(rmCuz.Value);
                    }
                    else
                    {
                        rsm.OnError(rmCuz.ToResultModel());
                    }
                }
                else
                {
                    rsm.OnError(ErrorResources.UserHasNotAuthorizationToPerformThisAction, EnumErrorCode.INSUFFICIENT_PRIVILEGES);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultServiceModel<File> GetCustomerFileForProjectGeneralSpace(long fileId, long projectId, string userName)
        {
            IResultServiceModel<File> rsm = new ResultServiceModel<File>();

            try
            {
                var isAuthorized = this.accountService.IsUserInRole(userName, RoleDefinitions.Organizer);

                if (isAuthorized)
                {
                    var rmFile = this.fileRepository.FindGSByIdAndProject(fileId, projectId);

                    if (rmFile.Success)
                    {
                        var file = rmFile.Value;

                        rsm.OnSuccess(file);
                    }
                    else
                    {
                        rsm.OnError(rmFile.ToResultModel());
                    }
                }
                else
                {
                    rsm.OnError(ErrorResources.UserHasNotAuthorizationToPerformThisAction, EnumErrorCode.INSUFFICIENT_PRIVILEGES);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;

        }
        public IResultServiceModel<File> GetCustomerFile(long fileId, string userName)
        {
            IResultServiceModel<File> rsm = new ResultServiceModel<File>();

            try
            {

                var rmFile = this.fileRepository.GetCompleteById(fileId);

                if (rmFile.Success)
                {
                    var file = rmFile.Value;

                    rsm.OnSuccess(file);
                }
                else
                {
                    rsm.OnError(rmFile.ToResultModel());
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;

        }
        public async Task<IResultServiceModel<ChangeSpaceStatusResponse>> ChangeCustomerSpaceStatus(ChangeSpaceStatusRequest request, string hostAddress) 
        {
            
            var rsmCuz = this.customerRepository.FindByIdAndProject(request.customerId, request.projectId);
            IResultServiceModel<ChangeSpaceStatusResponse> rsm = new ResultServiceModel<ChangeSpaceStatusResponse>();
            ChangeSpaceStatusResponse response = new ChangeSpaceStatusResponse();
            bool doChange = true;
            IResultModel rmUpdate = new ResultModel();

            try
            {
                if (rsmCuz.Success)
                {
                    var cuz = rsmCuz.Value;

                    response.index = request.index;                    

                    if ((EnumCustomerSpaceStatus)request.newStatus != EnumCustomerSpaceStatus.Free)
                    {
                        if (cuz.Reserved || cuz.Registered)
                        {

                            if (cuz.Email.ToLower() == request.email.ToLower() && !cuz.Registered)
                            {
                                doChange = true;
                            }
                            else
                            { 
                                rsm.OnError(CustomerResources.SpaceIsNotFree_Format.sf(cuz.SignUpCode, cuz.Name, cuz.Email), EnumErrorCode.SPACE_IS_NOT_FREE);                            
                            
                                response.email = cuz.Email;
                                response.registered = cuz.Registered;
                                response.reserved = cuz.Reserved;
                            
                                rsm.Value = response;
                                doChange = false;
                            
                            }

                        }
                    }                    
                    else 
                    {
                        doChange = true;
                    }


                    if (doChange)
                    {
                        if (request.newStatus == (int)EnumCustomerSpaceStatus.Assigned)
                        { 
                            var newReg = new CustomerRegistrationRequest(){
                                confirmPassword = request.password, 
                                password = request.password, 
                                projectCode = cuz.SignUpCode, 
                                userName = request.email
                            };

                            var rmReg = this.RegisterNewCustomerInProject(newReg, false);
                            if (rmReg.Success)
                            {

                                if (rmReg.Value.Email == request.email)
                                {
                                    response.email = request.email;
                                    response.registered = true;
                                    response.reserved = true;
                                    response.newSpaceStatus = (int)EnumCustomerSpaceStatus.Assigned;

                                    this.mailingService.SendEmailSpaceRegisteredForCustomer(cuz, hostAddress, request.email, request.password);

                                    rsm.OnSuccess(response);
                                }
                                else
                                {
                                    rsm.OnError(CustomerResources.ErrorOnManagerAssignSpaceToUser, rmReg.ErrorCode);
                                }
                            }
                            else 
                            {
                                rsm.OnError(rmReg.ErrorMessage, rmReg.ErrorCode);
                            }
                        }
                        else if (request.newStatus == (int)EnumCustomerSpaceStatus.Reserved)
                        {
                            cuz.Email = request.email;                            
                            cuz.Reserved = true;
                            cuz.Registered = false;
                            response.newSpaceStatus = (int)EnumCustomerSpaceStatus.Reserved;
                            this.customerRepository.Update(cuz);
                            rmUpdate = this.customerRepository.Save();
                            if (rmUpdate.Success)
                            {
                                response.email = request.email;
                                response.registered = false;
                                response.reserved = true;

                                this.mailingService.SendEmailSpaceReservedForCustomer(cuz, hostAddress, request.email);

                                rsm.OnSuccess(response);
                            }
                            else
                            {
                                rsm.OnError(CustomerResources.ErrorOnManagerReserveSpaceToUser, rmUpdate.ErrorCode);
                            }
                        }
                        else //Free space
                        {
                            var rmFiles = this.fileRepository.RemoveAllByCustomer(cuz.Id);
                            var userTask = this.accountService.FindUserByIdAsync(cuz.UserId);

                            cuz.Reserved = false;
                            cuz.Registered = false;
                            cuz.PasswordHash = "";
                            cuz.Email = null;                            
                            cuz.UserId = null;

                            this.customerRepository.Update(cuz);
                            rmUpdate = this.customerRepository.Save();

                            if (rmUpdate.Success)
                            {
                                var user = await userTask;
                                if (user != null) 
                                {
                                    var removeTask = this.accountService.Remove(user);
                                    await removeTask;
                                }

                                response.email = "";
                                response.registered = false;
                                response.reserved = false;
                                response.newSpaceStatus = (int)EnumCustomerSpaceStatus.Free;

                                rsm.OnSuccess(response);
                            }
                            else 
                            {
                                rsm.OnError(rmUpdate);
                            }
                        }                        
                    }
                    //rsm.OnSuccess(response);
                }
                else 
                {
                    rsm.OnError(rsmCuz.ErrorMessage, rsmCuz.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);                
            }

            return rsm;
        }
        public async Task<IResultServiceModel<CustomerPOCO>> GetCustomerAsync(long projectId, long customerId, string userName)
        {
            return await Task.FromResult(this.GetCustomer(projectId, customerId, userName));
        }
        public IResultServiceModel<long> GetMostRecentProjectId()
        {
            var rm = this.projectRepository.GetMostRecent();
            return rm;
        }
        public async Task<IResultServiceModel<FileResponse>> GetFileAsync(long fileId, string userName, long projectId)
        {
            IResultServiceModel<FileResponse> rsm = new ResultServiceModel<FileResponse>();

            try
            {
                var user = await this.accountService.FindUserByUserNameAsync(userName);
                if (user != null)
                {
                    var rsFile = this.fileRepository.FindByIdAndUser(fileId, user.Id);

                    if (!rsFile.Success && this.accountService.IsUserInRoleById(user.Id, RoleDefinitions.Organizer) && projectId > 0)
                    {
                        //User is admin and file is in General Space

                        rsFile = this.fileRepository.FindGSByIdAndProject(fileId, projectId);
                    }

                    if (rsFile.Success)
                    {
                        var fr = new FileResponse();
                        fr.file = new FilePOCO(rsFile.Value);

                        rsm.OnSuccess(fr);

                    }
                    else
                    {
                        rsm.OnError(rsFile.ErrorMessage, rsFile.ErrorCode);
                    }
                }
                else
                {
                    rsm.OnError(ErrorResources.UserNoActiveAccount, EnumErrorCode.INSUFFICIENT_PRIVILEGES);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultServiceModel<CustomerPOCO> GetReservedCustomer(long id)
        {
            var rsm = new ResultServiceModel<CustomerPOCO>();

            try
            {
                var rmCuz = this.customerRepository.Find(id);
                if (rmCuz.Success)
                {
                    var cuz = rmCuz.Value;

                    if (cuz.Reserved && !cuz.Registered && !string.IsNullOrWhiteSpace(cuz.Email))
                    {
                        rsm.OnSuccess(new CustomerPOCO(cuz));
                    }
                    else 
                    {
                        rsm.OnError(CustomerResources.ErrorReservedCustomerNotValid, EnumErrorCode.FILE_NOT_FOUND);
                    }
                }
                else
                {
                    rsm.OnError(rmCuz.ErrorMessage, rmCuz.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        public void Dispose()
        {
            this.accountService.Dispose();
        }

        //Privates
        private IResultServiceModel<File> MoveHashesToCustomerArea(FileUpload fu, string hostRoot)
        {
            //FileEndUploadResponse fur = new FileEndUploadResponse();
            IResultServiceModel<File> response = new ResultServiceModel<File>();
            int x = 0;
            string[] folders = new string[] { UploadFoldersEnum.CustomerFiles.ToString(), fu.CustomerId.ToString(), fu.ChannelId },
                tempFolders = new string[] { UploadFoldersEnum.Tmp.ToString(), fu.ChannelId };

            string filePath = string.Empty, fileDestination = string.Empty;
            try
            {

                var rmCuz = this.customerRepository.GetById(fu.CustomerId);
                IResultServiceModel<WFWorkflow> rmWf = new ResultServiceModel<WFWorkflow>();
                WFWorkflow workflow = null;
                if (rmCuz.Success)
                {
                    if (rmCuz.Value.SpaceNumber == 0)
                    {
                        rmWf = this.workflowService.GetWorkflowByCode(this.appConfigurationService.DefaultGeneralSpaceFileWorkflowCode);
                    }
                    else
                    {
                        rmWf = this.workflowService.GetWorkflowByCode(this.appConfigurationService.DefaultFileWorkflowCode);
                    }
                }
                else
                {
                    rmWf = this.workflowService.GetWorkflowByCode(this.appConfigurationService.DefaultFileWorkflowCode);
                }

                File file = new File();
                file.CustomerId = fu.CustomerId;
                file.FileTypeId = fu.FileTypeId;
                file.FirstDeliveryDate = fu.StartDate;
                file.Name = fu.CustomerLogicalName;
                file.Size = fu.Size;
                file.ChannelId = fu.ChannelId;
                file.MimeType = fu.MimeType;
                file.FileName = fu.Name;
                file.Version = 1;
                file.FileNotes = fu.FileNotes;

                if (rmWf.Success)
                {
                    workflow = rmWf.Value;
                    file.WFWorkflowId = workflow.Id;
                }

                fileDestination = this.fileService.GetRelativePath(fu.Name, folders);
                var rmDir = this.fileService.EnsureDirectoryExists(folders);

                using (var output = System.IO.File.Create(this.fileService.GetPhysicalPath(fileDestination)))
                {
                    for (x = 1; x <= fu.PartsTotal; x++)
                    {
                        filePath = this.fileService.GetRelativePath(string.Format("{0:000000}", x), tempFolders);

                        //filePath = System.IO.Path.Combine(UploadFoldersEnum.Tmp.ToString(), fu.ChannelId, string.Format("{0:000000}",x));
                        var res = this.fileService.GetPath(filePath);

                        using (var input = System.IO.File.OpenRead(res))
                        {
                            var buffer = new byte[input.Length];
                            int bytesRead;
                            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                output.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }


                file.URL = fileDestination.Replace("~/", hostRoot);
                this.fileRepository.Add(file);
                var rmSave = this.fileRepository.Save();
                if (rmSave.Success)
                {
                    //fur.channelId = fu.ChannelId;
                    //fur.url = file.URL;

                    //Remove all Temporary files
                    IResultModel rmDeleteTemp = this.fileUploadRepository.RemoveAllCompleted();
                    rmDeleteTemp = this.fileService.DeleteFolderPhysicalPath(tempFolders);
                    //Retake the file including related fields

                    var rmFile = this.fileRepository.GetCompleteById(file.Id);
                    if (rmFile.Success)
                    {
                        response.OnSuccess(rmFile.Value);
                    }
                    else
                    {
                        if (file.WFWorkflow == null && workflow != null)
                        {
                            file.WFWorkflow = workflow;
                        }

                        response.OnSuccess(file);
                    }
                }
                else
                {
                    response.OnError(rmSave.ErrorMessage, rmSave.ErrorCode);
                }

            }
            catch (Exception ex)
            {
                response.OnException(ex);
            }


            return response;
        }
        private IResultServiceModel<File> MoveHashesToExistingCustomerArea(FileUpload fu, long fileId, string hostRoot)
        {
            //FileEndUploadResponse fur = new FileEndUploadResponse();
            IResultServiceModel<File> response = new ResultServiceModel<File>();
            int x = 0;
            string[] folders = new string[] { UploadFoldersEnum.CustomerFiles.ToString(), fu.CustomerId.ToString(), fu.ChannelId },
                tempFolders = new string[] { UploadFoldersEnum.Tmp.ToString(), fu.ChannelId };
            File file = new File();
            string filePath = string.Empty, fileDestination = string.Empty;

            try
            {
                fileDestination = this.fileService.GetRelativePath(fu.Name, folders);

                var rmfile = this.fileRepository.GetCompleteById(fileId);
                if (rmfile.Success)
                {
                    file = rmfile.Value;
                    file.Version = file.Version + 1;
                    file.Size = fu.Size;

                    var rmDir = this.fileService.EnsureDirectoryExists(folders);

                    using (var output = System.IO.File.Create(this.fileService.GetPhysicalPath(fileDestination)))
                    {
                        for (x = 1; x <= fu.PartsTotal; x++)
                        {

                            filePath = this.fileService.GetRelativePath(string.Format("{0:000000}", x), tempFolders);

                            //filePath = System.IO.Path.Combine(UploadFoldersEnum.Tmp.ToString(), fu.ChannelId, string.Format("{0:000000}",x));
                            var res = this.fileService.GetPath(filePath);

                            using (var input = System.IO.File.OpenRead(res))
                            {
                                var buffer = new byte[input.Length];
                                int bytesRead;
                                while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    output.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }


                    //file.URL = fileDestination.Replace("~/", hostRoot);
                    this.fileRepository.Update(file);
                    var rmSave = this.fileRepository.Save();

                    if (rmSave.Success)
                    {
                        //Remove all Temporary files
                        IResultModel rmDeleteTemp = this.fileUploadRepository.RemoveAllCompleted();
                        rmDeleteTemp = this.fileService.DeleteFolderPhysicalPath(tempFolders);

                        //Retake the file including related fields

                        var rmFile = this.fileRepository.GetCompleteById(file.Id);
                        if (rmFile.Success)
                        {
                            response.OnSuccess(rmFile.Value);
                        }
                        else
                        {
                            if (file.WFWorkflowId.HasValue)
                            {
                                IResultServiceModel<WFWorkflow> rmWf = new ResultServiceModel<WFWorkflow>();
                                rmWf = this.workflowService.GetWorkflowById(file.WFWorkflowId.Value);

                                if (rmWf.Success)
                                {
                                    file.WFWorkflow = rmWf.Value;
                                }
                            }

                            response.OnSuccess(file);
                        }
                    }
                    else
                    {
                        response.OnError(rmSave.ErrorMessage, rmSave.ErrorCode);
                    }
                }
                else
                {
                    response.OnError(rmfile.ToResultModel());
                }
            }
            catch (Exception ex)
            {
                response.OnException(ex);
            }


            return response;
        }
        private IResultModel ReportManagerUsersFileStateChanged(File file, User user, WFState wfState, string host)
        {
            IResultModel rm = new ResultModel();
            var now = DateTime.Now;
            IList<string> listOfEmails = new List<string>();
            string mailTemplate = string.Empty, mailSubject = string.Empty;
            string detailUrl = string.Format("{0}/#/file/activity/{1}", host, file.Id);
            string loginUrl = string.Format("{0}/#/login", host);


            if (wfState.Code == GrafidecStateEnum.FILE_IN_UPLOAD.ToString() ||
                wfState.Code == GrafidecStateEnum.FILE_IN_REVISION.ToString() ||
                wfState.Code == GrafidecStateEnum.FILE_LOADED.ToString())
            {
                string[] mailParams = {
                                          file.Name, 
                                          file.FileName,
                                          user.Email,
                                          detailUrl,
                                          file.URL,
                                          loginUrl
                                      };
                mailTemplate = EmailTemplateResources.WFNewFileInPlatform_Format.sf(mailParams);
                mailSubject = EmailTemplateResources.WFFileAddedSubject;
            }
            else
            {
                string[] mailParams = {
                                          file.Name, 
                                          file.FileName,
                                          user.Email,
                                          detailUrl,
                                          file.URL,
                                          loginUrl,
                                          wfState.Name
                                      };
                mailTemplate = EmailTemplateResources.WFFileStateChangedTo_Format.sf(mailParams);
                mailSubject = EmailTemplateResources.WFFileStateChangeSubject;
            }

            listOfEmails = new List<string>(this.GetEmailAddressesOfUsersWithRole(RoleDefinitions.Supervisor));
            listOfEmails.Add(user.Email);

            var rmMail = this.mailingService.SendReportToUsers(mailSubject, listOfEmails.ToArray(), mailTemplate);

            if (rmMail.Success)
            {
                rm.OnSuccess();
            }
            else
            {
                rm.OnError(rmMail);
            }

            return rm;
        }
        private async Task<IResultModel> ReportManagerUsersFileStateChangedAsync(File file, User user, WFState wfState, string host)
        {
            return this.ReportManagerUsersFileStateChanged(file, user, wfState, host);
        }
        private IEnumerable<string> GetEmailAddressesOfUsersWithRole(string roleId)
        {
            try
            {
                var list = this.accountService.GetUserEmailsInRole(roleId);
                return list;
            }
            catch (Exception)
            {
                return new List<string>();

            }

        }


    }
}
