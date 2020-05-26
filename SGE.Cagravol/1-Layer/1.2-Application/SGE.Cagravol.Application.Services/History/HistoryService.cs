using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.JSON.Files;
using SGE.Cagravol.Domain.POCO.Files;
using SGE.Cagravol.Domain.POCO.Workflows;
using SGE.Cagravol.Domain.Repositories.Files;
using SGE.Cagravol.Domain.Repositories.History;
using SGE.Cagravol.Presentation.Resources.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.History
{
    public class HistoryService
        : IHistoryService
    {
        private readonly IFileRepository fileRepository;
        private readonly IWFFileStateRepository wfFileStateRepository;
        private readonly IWFFileStateNoteRepository wfFileStateNoteRepository;

        public HistoryService(IFileRepository fileRepository,
            IWFFileStateRepository wfFileStateRepository,
            IWFFileStateNoteRepository wfFileStateNoteRepository)
        {
            this.fileRepository = fileRepository;
            this.wfFileStateRepository = wfFileStateRepository;
            this.wfFileStateNoteRepository = wfFileStateNoteRepository;
        }

        public IResultServiceModel<FileHistoryResponse> GetFileHistoryByFileAndUser(long fileId, User user, bool isOnlyCustomer)
        {
            IResultServiceModel<FileHistoryResponse> rsm = new ResultServiceModel<FileHistoryResponse>();
            FileHistoryResponse response = new FileHistoryResponse();
            bool authorized = true;
            try
            {

                var rmFile = this.fileRepository.Find(fileId);
                if (rmFile.Success)
                {
                    var file = rmFile.Value;
                    if (isOnlyCustomer)
                    {
                        //Authorization depends on whether the user is the owner or not
                        authorized = (file.Customer.UserId == user.Id);
                    }

                    if (authorized)
                    {
                        var rmStates = this.wfFileStateRepository.GetListByFileId(fileId);
                        response.File = new FilePOCO(file);

                        if (rmStates.Success)
                        {
                            response.States = rmStates.Value.Select(s => new WFFileStatePOCO(s));
                        }
                        else
                        {
                            response.States = Enumerable.Empty<WFFileStatePOCO>();
                        }

                        rsm.OnSuccess(response);
                    }
                }
                else
                {
                    rsm.OnError(CustomerResources.UserHasNotAuthorizationForThisFileHistory);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        public IResultServiceModel<FileStateCommentResponse> AddCommentToFileState(FileStateCommentRequest request, User user)
        {

            IResultServiceModel<FileStateCommentResponse> rsm = new ResultServiceModel<FileStateCommentResponse>();

            try
            {
                var stateNote = new WFFileStateNote()
                {
                    TS = request.ts,
                    Comment = request.comment,
                    UserId = user.Id,
                    WFEntityStateId = request.stateId
                };

                this.wfFileStateNoteRepository.Add(stateNote);
                var rm = this.wfFileStateNoteRepository.Save();
                if (rm.Success)
                {
                    rsm.OnSuccess(new FileStateCommentResponse()
                    {
                        fileId = request.fileId,
                        stateId = request.stateId,
                        newId = stateNote.Id,
                        temporalId = request.id
                    });
                }
                else
                {
                    rsm.OnError(rm);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;

        }
    }
}
