using SGE.Cagravol.Application.Core.Helpers;
using SGE.Cagravol.Application.Services.Identity;
using SGE.Cagravol.Application.Services.Logs;
using SGE.Cagravol.Application.Services.Utils;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Domain.JSON.Account;
using SGE.Cagravol.Domain.JSON.Alfred;
using SGE.Cagravol.Domain.JSON.Files;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.POCO.Customers;
using SGE.Cagravol.Domain.JSON.Workflows;
using SGE.Cagravol.Domain.POCO.Files;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.JSON.Projects;

namespace SGE.Cagravol.Application.Services.WebApi
{
    public interface IWebApiContentService : IDisposable
    {
        IHtmlToViewHelper HtmlToViewHelper { get; }
        ILogService LogService { get; }
        IUtilService UtilService { get; }
        IAccountService AccountService { get; }
        Task<IResultServiceModel<Customer>> RegisterNewCustomerInProjectAsync(CustomerRegistrationRequest request, bool mustBeFree = true);
        IResultServiceModel<Customer> RegisterNewCustomerInProject(CustomerRegistrationRequest request, bool mustBeFree = true);
        Task<IResultServiceModel<UserLoginResponse>> UserLoginAsync(UserLoginRequest request);
        IResultServiceModel<UserLoginResponse> UserLogin(UserLoginRequest request);
        IResultModel SendEmailNewCustomerSignUp(Customer customer, string hostAddress);
        IResultModel SendTestEmail(string email, string message);
        Task<IResultServiceModel<AlfredDefaultPlatformParametersResponse>> GetDefaultPlatformParametersAsync();
        IResultServiceModel<AlfredDefaultPlatformParametersResponse> GetDefaultPlatformParameters();
        IResultServiceModel<Customer> CheckProjectForLoggedCustomer(string userId);
        Task<IResultServiceModel<Customer>> CheckProjectForLoggedCustomerByName(string userId);
        Task<IResultServiceModel<FileUploadResponse>> FileUploadInitAsync(FileUploadRequest request);
        IResultServiceModel<FileUploadResponse> FileUploadInit(FileUploadRequest request);
        Task<IResultServiceModel<FileHashUploadResponse>> UploadFileHashAsync(FileHashUploadRequest request);
        IResultServiceModel<FileHashUploadResponse> UploadFileHash(FileHashUploadRequest request);
        IResultServiceModel<WorkflowMoveFileResponse> MoveWorkflowStateForFile(WorkflowMoveFileRequest request);
        Task<IResultServiceModel<WorkflowMoveFileResponse>> MoveWorkflowStateForFileAsync(WorkflowMoveFileRequest request);
        Task<IResultServiceModel<FileEndUploadResponse>> EndFileUploadAsync(FileEndUploadRequest request, string hostRoot);
        IResultServiceModel<FileEndUploadResponse> EndFileUpload(FileEndUploadRequest request, string hostRoot);
        Task<IResultServiceModel<FileEndUploadResponse>> EndFileResendUploadAsync(FileEndUploadRequest request, string hostRoot);
        IResultServiceModel<FileEndUploadResponse> EndFileResendUpload(FileEndUploadRequest request, string hostRoot);
        IResultModel UpdateFileForProjectGeneralSpace(long id, string name, long fileTypeId, long projectId, string userName);
        IResultModel UpdateFile(long id, string name, string notes, long fileTypeId, string userName);
        IResultModel FileCancelUpload(FileCancelUploadRequest request);
        Task<IResultServiceModel<FileHistoryResponse>> GetFileHistoryAsync(string userName, long id);
        Task<IResultServiceModel<FileStateCommentResponse>> AddCommentToFileStateAsync(FileStateCommentRequest request);
        IResultServiceModel<CustomerPOCO> GetCustomer(long projectId, long customerId, string userName);
        IResultServiceModel<Customer> GetCustomerForProjectGeneralSpace(long projectId, string userName);
        Task<IResultServiceModel<CustomerPOCO>> GetCustomerAsync(long projectId, long customerId, string userName);
        Task<IResultServiceModel<FileResponse>> GetFileAsync(long fileId, string userName, long projectId);
        IResultServiceModel<long> GetMostRecentProjectId();
        Task<IResultServiceModel<ChangeSpaceStatusResponse>> ChangeCustomerSpaceStatus(ChangeSpaceStatusRequest request, string hostAddress);
        IResultServiceModel<File> GetCustomerFileForProjectGeneralSpace(long fileId, long projectId, string userName);
        IResultServiceModel<File> GetCustomerFile(long fileId, string userName);

        IResultServiceModel<CustomerPOCO> GetReservedCustomer(long id);

    }
}
