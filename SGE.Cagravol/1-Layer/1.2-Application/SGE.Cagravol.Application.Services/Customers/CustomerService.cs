using SGE.Cagravol.Application.Core.Helpers;
using SGE.Cagravol.Application.Services.Async;
using SGE.Cagravol.Application.Services.Common;
using SGE.Cagravol.Domain.JSON.Customers;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Customers;
using SGE.Cagravol.Domain.Repositories.Files;
using SGE.Cagravol.Presentation.Resources.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.POCO.Files;

namespace SGE.Cagravol.Application.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IFileRepository fileRepository;
        private readonly IAsyncService asyncService;
        private readonly IFileService fileService;

        public CustomerService(ICustomerRepository customerRepository, 
            IFileRepository fileRepository,
            IAsyncService asyncService,
            IFileService fileService)
        {
            this.customerRepository = customerRepository;
            this.fileRepository = fileRepository;
            this.asyncService = asyncService;
            this.fileService = fileService;
        }

        public IResultModel DeleteFile(CustomerFileDeleteRequest request)
        {
            return this.asyncService.RunSync(() => this.DeleteFileAsync(request));
        }

        public async Task<IResultModel> DeleteFileAsync(CustomerFileDeleteRequest request)
        {
            IResultModel rm = new ResultModel();
            string customerId = string.Empty, channelId = string.Empty;

            try
            {
                var rmGet = this.fileRepository.Find(request.id);
                if (rmGet.Success)
                {
                    var item = rmGet.Value;

                    customerId = item.CustomerId.ToString();
                    channelId = item.ChannelId.ToString();

                    if (item.Customer.Email == request.userName)
                    {
                        this.fileRepository.Remove(item);
                        var rmDelete = await this.fileRepository.SaveAsync();

                        if (rmDelete.Success)
                        {

                            //Elimina los archivos 
                            string[] folders = new string[] 
                            { 
                                UploadFoldersEnum.CustomerFiles.ToString(),
                                customerId, 
                                channelId
                            };

                            var rmDeleteFiles = this.fileService.DeleteFolderPhysicalPath(folders);
                            if (rmDeleteFiles.Success)
                            {
                                rm.OnSuccess();
                            }
                            else {
                                rm.OnError(rmDeleteFiles.ErrorMessage, rmDeleteFiles.ErrorCode);
                            }
                        }
                        else
                        {
                            rm.OnError(rmDelete.ErrorMessage, rmDelete.ErrorCode);
                        }
                    }
                    else
                    {
                        rm.OnError(CustomerResources.ErrorItemDoesNotExist_OnDelete);
                    }
                }
                else 
                {
                    rm.OnError(rmGet.ErrorMessage, rmGet.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }
            
            return rm;
        }

        public async Task<IResultModel> DeleteGeneralSpaceFileAsync(CustomerFileDeleteRequest request)
        {
            IResultModel rm = new ResultModel();
            string customerId = string.Empty, channelId = string.Empty;

            try
            {
                var rmGet = this.fileRepository.Find(request.id);
                if (rmGet.Success)
                {
                    var item = rmGet.Value;

                    customerId = item.CustomerId.ToString();
                    channelId = item.ChannelId.ToString();

                    if (item.Customer == null)
                    {
                        var rmCuz = this.customerRepository.Find(item.CustomerId);
                        if (rmCuz.Success)
                        {
                            item.Customer = rmCuz.Value;
                        }
                    }

                    if (item.Customer != null && item.Customer.IsGeneric && item.Customer.SpaceNumber == 0)
                    {
                        this.fileRepository.Remove(item);
                        var rmDelete = await this.fileRepository.SaveAsync();

                        if (rmDelete.Success)
                        {

                            //Elimina los archivos 
                            string[] folders = new string[] 
                            { 
                                UploadFoldersEnum.CustomerFiles.ToString(),
                                customerId, 
                                channelId
                            };

                            var rmDeleteFiles = this.fileService.DeleteFolderPhysicalPath(folders);
                            if (rmDeleteFiles.Success)
                            {
                                rm.OnSuccess();
                            }
                            else
                            {
                                rm.OnError(rmDeleteFiles.ErrorMessage, rmDeleteFiles.ErrorCode);
                            }
                        }
                        else
                        {
                            rm.OnError(rmDelete.ErrorMessage, rmDelete.ErrorCode);
                        }
                    }
                    else
                    {
                        rm.OnError(CustomerResources.ErrorItemDoesNotExist_OnDelete);
                    }
                }
                else
                {
                    rm.OnError(rmGet.ErrorMessage, rmGet.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }

        public IEnumerable<FilePOCO> ExtractCustomerFileList(ICollection<File> fileCollection)
        {
            return fileCollection.Select(s => new FilePOCO(s));
        }        
    }
}
